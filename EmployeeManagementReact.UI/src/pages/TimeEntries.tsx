import React, { useState, useEffect, ChangeEvent } from 'react';
import { Employee, Project, TimeEntry } from '../types/types';
import { mockEmployees, mockProjects, mockTimeEntries } from '../data/mockData';
import './TimeEntries.css';

const TimeEntries: React.FC = () => {
    const [selectedEmployee, setSelectedEmployee] = useState<number>();
    const [weekDates, setWeekDates] = useState<string[]>([]);
    const [timeEntries, setTimeEntries] = useState<TimeEntry[]>([]);
    const [projectEntries, setProjectEntries] = useState<string[]>([]);
    const [comments, setComments] = useState<{ [key: string]: string }>({});

    useEffect(() => {
        // Calculate week dates based on current date
        const today = new Date();
        const monday = new Date(today);
        monday.setDate(monday.getDate() - monday.getDay() + 1);
        
        const dates = Array.from({ length: 7 }, (_, i) => {
            const date = new Date(monday);
            date.setDate(monday.getDate() + i);
            return date.toISOString().split('T')[0];
        });
        setWeekDates(dates);

        // Load initial time entries
        if (selectedEmployee) {
            const entries = mockTimeEntries.filter(entry => entry.employeeId === selectedEmployee);
            setTimeEntries(entries);
        }
    }, [selectedEmployee]);

    const handleHoursChange = (projectId: string, date: string, hours: number) => {
        const newEntry: TimeEntry = {
            id: Math.random(), // In real app, this would come from backend
            employeeId: selectedEmployee!,
            projectId,
            date,
            hours,
            comment: comments[`${projectId}-${date}`]
        };

        setTimeEntries(prev => {
            const filtered = prev.filter(e => 
                !(e.projectId === projectId && e.date === date)
            );
            return [...filtered, newEntry];
        });
    };

    const handleCommentChange = (projectId: string, date: string, comment: string) => {
        setComments(prev => ({
            ...prev,
            [`${projectId}-${date}`]: comment
        }));
    };

    const addNewProjectEntry = (projectId: string) => {
        if (!projectEntries.includes(projectId)) {
            setProjectEntries(prev => [...prev, projectId]);
        }
    };

    const getEntryForCell = (projectId: string, date: string) => {
        return timeEntries.find(entry => 
            entry.projectId === projectId && entry.date === date
        );
    };

    return (
        <div className="time-entries-container">
            <div className="time-entries-header">
                <div className="employee-selector">
                    <label>Employee:</label>
                    <select
                        className="select-input"
                        onChange={(e: ChangeEvent<HTMLSelectElement>) => setSelectedEmployee(Number(e.target.value))}
                        value={selectedEmployee || ''}
                    >
                        <option value="">Select Employee</option>
                        {mockEmployees.map(emp => (
                            <option key={emp.id} value={emp.id}>
                                {emp.name}
                            </option>
                        ))}
                    </select>
                </div>

                <div className="date-range">
                    <span>{weekDates[0]} - {weekDates[6]}</span>
                </div>
            </div>

            <div className="time-entries-grid">
                <table>
                    <thead>
                        <tr>
                            <th>Project ID</th>
                            <th>Project Name</th>
                            {weekDates.map(date => (
                                <th key={date}>{new Date(date).toLocaleDateString('en-US', { weekday: 'short' })}</th>
                            ))}
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr className="add-new-row">
                            <td colSpan={2}>
                                <select
                                    className="select-input"
                                    onChange={(e: ChangeEvent<HTMLSelectElement>) => addNewProjectEntry(e.target.value)}
                                >
                                    <option value="">Select Project</option>
                                    {mockProjects.map(project => (
                                        <option key={project.id} value={project.id}>
                                            {project.name}
                                        </option>
                                    ))}
                                </select>
                            </td>
                            {weekDates.map((_, index) => <td key={index}></td>)}
                            <td></td>
                        </tr>
                        {projectEntries.map(projectId => {
                            const project = mockProjects.find(p => p.id === projectId);
                            const weekTotal = weekDates.reduce((sum, date) => {
                                const entry = getEntryForCell(projectId, date);
                                return sum + (entry?.hours || 0);
                            }, 0);

                            return (
                                <tr key={projectId}>
                                    <td>{project?.id}</td>
                                    <td>{project?.name}</td>
                                    {weekDates.map(date => {
                                        const entry = getEntryForCell(projectId, date);
                                        return (
                                            <td key={date} className="time-cell">
                                                <input
                                                    type="number"
                                                    className="number-input"
                                                    value={entry?.hours || ''}
                                                    onChange={(e: ChangeEvent<HTMLInputElement>) => 
                                                        handleHoursChange(projectId, date, parseFloat(e.target.value))
                                                    }
                                                    step={0.5}
                                                    min={0}
                                                    max={24}
                                                />
                                                <button
                                                    className="comment-button"
                                                    title="Add comment"
                                                    onClick={() => {
                                                        const comment = prompt('Enter comment:', entry?.comment || '');
                                                        if (comment !== null) {
                                                            handleCommentChange(projectId, date, comment);
                                                            handleHoursChange(projectId, date, entry?.hours || 0);
                                                        }
                                                    }}
                                                >
                                                    💬
                                                </button>
                                            </td>
                                        );
                                    })}
                                    <td>{weekTotal}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default TimeEntries; 
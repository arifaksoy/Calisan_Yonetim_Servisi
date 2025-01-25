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
    const [selectedProject, setSelectedProject] = useState<string>('');

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
            
            // Get unique project IDs from entries
            const uniqueProjectIds = Array.from(new Set(entries.map(entry => entry.projectId)));
            setProjectEntries(uniqueProjectIds);
        }
    }, [selectedEmployee]);

    const handleAddNewEntry = () => {
        if (!selectedEmployee || !selectedProject) {
            alert('Please select an employee and project');
            return;
        }

        // Add project to entries if not exists
        if (!projectEntries.includes(selectedProject)) {
            setProjectEntries(prev => [...prev, selectedProject]);
        }

        // Reset form
        setSelectedProject('');
    };

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

    const handleDeleteProject = (projectId: string) => {
        // Remove project from entries
        setProjectEntries(prev => prev.filter(id => id !== projectId));
        
        // Remove all time entries for this project
        setTimeEntries(prev => prev.filter(entry => entry.projectId !== projectId));
        
        // Remove all comments for this project
        const newComments = { ...comments };
        Object.keys(newComments).forEach(key => {
            if (key.startsWith(`${projectId}-`)) {
                delete newComments[key];
            }
        });
        setComments(newComments);
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

            <div className="new-entry-form">
                <select
                    className="select-input"
                    value={selectedProject}
                    onChange={(e: ChangeEvent<HTMLSelectElement>) => setSelectedProject(e.target.value)}
                >
                    <option value="">Select Project</option>
                    {mockProjects
                        .filter(project => !projectEntries.includes(project.id))
                        .map(project => (
                            <option key={project.id} value={project.id}>
                                {project.name}
                            </option>
                        ))
                    }
                </select>
                <button className="add-button" onClick={handleAddNewEntry}>
                    Add Entry
                </button>
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
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
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
                                                <div className="time-cell-content">
                                                    <input
                                                        type="number"
                                                        className="number-input"
                                                        value={entry?.hours || ''}
                                                        onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                                            const value = e.target.value;
                                                            if (value === '' || (parseFloat(value) >= 0 && parseFloat(value) <= 24)) {
                                                                handleHoursChange(projectId, date, parseFloat(value) || 0);
                                                            }
                                                        }}
                                                        onBlur={(e: React.FocusEvent<HTMLInputElement>) => {
                                                            const hours = parseFloat(e.target.value);
                                                            if (!isNaN(hours) && hours > 0) {
                                                                // Here you would make the API call to update the entry
                                                                console.log('Updating entry:', { projectId, date, hours });
                                                            }
                                                        }}
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
                                                                if (entry?.hours) {
                                                                    handleHoursChange(projectId, date, entry.hours);
                                                                }
                                                            }
                                                        }}
                                                    >
                                                        💬
                                                    </button>
                                                </div>
                                            </td>
                                        );
                                    })}
                                    <td>{weekTotal}</td>
                                    <td>
                                        <button
                                            className="delete-button"
                                            onClick={() => handleDeleteProject(projectId)}
                                            title="Delete project entries"
                                        >
                                            🗑️
                                        </button>
                                    </td>
                                </tr>
                            );
                        })}
                    </tbody>
                    <tfoot>
                        <tr className="grand-total-row">
                            <td colSpan={2}>Grand Total</td>
                            {weekDates.map(date => {
                                const dayTotal = projectEntries.reduce((sum, projectId) => {
                                    const entry = getEntryForCell(projectId, date);
                                    return sum + (entry?.hours || 0);
                                }, 0);
                                return <td key={date}>{dayTotal || ''}</td>;
                            })}
                            <td>
                                {projectEntries.reduce((sum, projectId) => {
                                    return sum + weekDates.reduce((daySum, date) => {
                                        const entry = getEntryForCell(projectId, date);
                                        return daySum + (entry?.hours || 0);
                                    }, 0);
                                }, 0)}
                            </td>
                            <td></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    );
};

export default TimeEntries; 
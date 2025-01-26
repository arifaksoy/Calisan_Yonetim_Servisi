import React, { useState, useEffect, ChangeEvent } from 'react';
import { Project, TimeEntry } from '../types/types';
import './TimeEntries.css';
import axios from 'axios';
import { jwtDecode } from "jwt-decode";
import { CircularProgress } from '@mui/material';

interface Employee {
    personnelId: string;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    userCompanyId: string;
    companyName: string;
    roleId: string;
    roleName: string;
}

interface DecodedToken {
    CompanyId: string;
}

interface TimeEntryRequest {
    timeEntriesDate: string;
    projectId: string;
    personnelId: string;
    amount: number;
}

interface TimeEntryPostRequest {
    timeEntries: TimeEntryRequest[];
}

const getTokenFromCookie = (): string | null => {
    const token = document.cookie
        .split('; ')
        .find(row => row.startsWith('auth_token='))
        ?.split('=')[1];
    return token || null;
};

const TimeEntries: React.FC = () => {
    const [selectedEmployee, setSelectedEmployee] = useState<string>();
    const [selectedProject, setSelectedProject] = useState<string>('');
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [projects, setProjects] = useState<Project[]>([]);
    const [weekDates, setWeekDates] = useState<string[]>([]);
    const [timeEntries, setTimeEntries] = useState<TimeEntry[]>([]);
    const [projectEntries, setProjectEntries] = useState<string[]>([]);
    const [loading, setLoading] = useState(true);

    const fetchEmployees = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            const response = await axios.get(`https://localhost:7076/api/v1/company/${companyId}/employee`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setEmployees(response.data);
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    };

    const fetchProjects = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            const response = await axios.get(`https://localhost:7076/api/v1/company/${companyId}/projects`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setProjects(response.data);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching projects:', error);
            setLoading(false);
        }
    };

    const fetchTimeEntries = async (startDate: string, endDate: string) => {
        try {
            const token = getTokenFromCookie();
            if (!token || !selectedEmployee) {
                return;
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            const response = await axios.get<TimeEntry[]>(
                `https://localhost:7076/api/v1/company/${companyId}/time-entries`, {
                    params: {
                        personnelId: selectedEmployee,
                        startDate,
                        endDate
                    },
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                }
            );

            setTimeEntries(response.data);
            
            // Get unique project IDs from entries
            const uniqueProjectIds = Array.from(new Set(response.data.map(entry => entry.projectId)));
            setProjectEntries(uniqueProjectIds);
        } catch (error) {
            console.error('Error fetching time entries:', error);
        }
    };

    useEffect(() => {
        Promise.all([fetchEmployees(), fetchProjects()]);
    }, []);

    useEffect(() => {
        // Get today's date
        const today = new Date();
        
        // Find the Monday of the current week (week starts on Monday)
        const monday = new Date(today);
        const currentDay = monday.getDay();
        
        // If today is Sunday (0), go back 6 days, otherwise go back (currentDay - 1) days
        const daysToSubtract = currentDay === 0 ? 6 : currentDay - 1;
        monday.setDate(today.getDate() - daysToSubtract);
        monday.setHours(0, 0, 0, 0);

        // Generate dates for the week (Monday to Sunday)
        const dates = Array.from({ length: 7 }, (_, i) => {
            const date = new Date(monday);
            date.setDate(monday.getDate() + i);
            return date.toISOString().split('T')[0];
        });
        setWeekDates(dates);

        // Load time entries when employee is selected
        if (selectedEmployee && dates.length > 0) {
            fetchTimeEntries(dates[0], dates[6]);
        }
    }, [selectedEmployee]);

    const handleAddNewEntry = async () => {
        if (!selectedEmployee || !selectedProject) {
            alert('Please select an employee and project');
            return;
        }

        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            // Create time entries for each day of the week
            const timeEntries: TimeEntryRequest[] = weekDates.map(date => ({
                timeEntriesDate: `${date}T00:00:00`,
                projectId: selectedProject,
                personnelId: selectedEmployee,
                amount: 0
            }));

            // Make the POST request
            await axios.post(
                `https://localhost:7076/api/v1/company/${companyId}/time-entries`,
                { timeEntries },
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            // After successful addition, fetch the updated data
            fetchTimeEntries(weekDates[0], weekDates[6]);

            // Add project to entries if not exists
            if (!projectEntries.includes(selectedProject)) {
                setProjectEntries(prev => [...prev, selectedProject]);
            }

            // Reset form
            setSelectedProject('');
        } catch (error) {
            console.error('Error adding time entries:', error);
            alert('Failed to add time entries. Please try again.');
        }
    };

    const handleHoursChange = async (projectId: string, date: string, amount: number) => {
        try {
            const entry = getEntryForCell(projectId, date);
            if (!entry) {
                console.error('No entry found for update');
                // Revert UI to original value
                setTimeEntries(prev => [...prev]);
                return;
            }

            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            const updateData = {
                timeEntriesDate: entry.timeEntriesDate,
                projectId: entry.projectId,
                personnelId: entry.personnelId,
                amount: amount
            };

            await axios.put(
                `https://localhost:7076/api/v1/company/${companyId}/time-entries/${entry.timeEntriesId}`,
                updateData,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );

            // After successful update, refresh the time entries
            fetchTimeEntries(weekDates[0], weekDates[6]);
        } catch (error) {
            console.error('Error updating time entry:', error);
            alert('Failed to update time entry. Please try again.');
            
            // Revert UI to original values by fetching the data again
            fetchTimeEntries(weekDates[0], weekDates[6]);
        }
    };

    const handleDeleteProject = async (projectId: string) => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            // Collect all timeEntriesIds for the project
            const timeEntriesIds = timeEntries
                .filter(entry => entry.projectId === projectId)
                .map(entry => entry.timeEntriesId);

            if (timeEntriesIds.length === 0) {
                console.error('No time entries found for deletion');
                return;
            }

            // Call the bulk delete endpoint
            await axios.delete(
                `https://localhost:7076/api/v1/company/${companyId}/time-entries/bulk`,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    },
                    data: timeEntriesIds
                }
            );

            // After successful deletion, update the UI
            setProjectEntries(prev => prev.filter(id => id !== projectId));
            setTimeEntries(prev => prev.filter(entry => entry.projectId !== projectId));
        } catch (error) {
            console.error('Error deleting time entries:', error);
            alert('Failed to delete time entries. Please try again.');
        }
    };

    const getEntryForCell = (projectId: string, date: string) => {
        return timeEntries.find(entry => 
            entry.projectId === projectId && 
            entry.timeEntriesDate.split('T')[0] === date
        );
    };

    if (loading) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                <CircularProgress />
            </div>
        );
    }

    return (
        <div className="time-entries-container">
            <div className="time-entries-header">
                <div className="employee-selector">
                    <label>Employee:</label>
                    <select
                        className="select-input"
                        onChange={(e: ChangeEvent<HTMLSelectElement>) => setSelectedEmployee(e.target.value)}
                        value={selectedEmployee || ''}
                    >
                        <option value="">Select Employee</option>
                        {employees.map(emp => (
                            <option key={emp.personnelId} value={emp.personnelId}>
                                {`${emp.firstName} ${emp.lastName}`}
                            </option>
                        ))}
                    </select>
                </div>

                <div className="date-range">
                    <span>
                        {new Date(weekDates[0]).toLocaleDateString('en-US', { month: 'long', day: 'numeric' })} - {new Date(weekDates[6]).toLocaleDateString('en-US', { month: 'long', day: 'numeric' })}
                    </span>
                </div>
            </div>

            <div className="new-entry-form">
                <select
                    className="select-input"
                    value={selectedProject}
                    onChange={(e: ChangeEvent<HTMLSelectElement>) => setSelectedProject(e.target.value)}
                >
                    <option value="">Select Project</option>
                    {projects
                        .filter(project => !projectEntries.includes(project.projectId) && project.status === 1)
                        .map(project => (
                            <option key={project.projectId} value={project.projectId}>
                                {project.projectName}
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
                            <th>Project Name</th>
                            {['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'].map((day, index) => (
                                <th key={weekDates[index]}>
                                    <div>{day}</div>
                                    <div>{new Date(weekDates[index]).getDate()}</div>
                                </th>
                            ))}
                            <th>Total</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {projectEntries.map(projectId => {
                            const project = projects.find(p => p.projectId === projectId);
                            const weekTotal = weekDates.reduce((sum, date) => {
                                const entry = getEntryForCell(projectId, date);
                                return sum + (entry?.amount || 0);
                            }, 0);

                            return (
                                <tr key={projectId}>
                                    <td>{project?.projectName}</td>
                                    {weekDates.map(date => {
                                        const entry = getEntryForCell(projectId, date);
                                        return (
                                            <td key={date} className="time-cell">
                                                <div className="time-cell-content">
                                                    <input
                                                        type="number"
                                                        className="number-input"
                                                        value={entry?.amount ?? ''}
                                                        onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                                            const value = e.target.value;
                                                            const numValue = parseFloat(value);
                                                            if (value === '' || (!isNaN(numValue) && numValue >= 0 && numValue <= 24)) {
                                                                const newTimeEntries = timeEntries.map(te => {
                                                                    if (te.projectId === projectId && te.timeEntriesDate.split('T')[0] === date) {
                                                                        return { ...te, amount: value === '' ? null : numValue };
                                                                    }
                                                                    return te;
                                                                });
                                                                setTimeEntries(newTimeEntries);
                                                            }
                                                        }}
                                                        onBlur={(e: React.FocusEvent<HTMLInputElement>) => {
                                                            const value = e.target.value;
                                                            const numValue = parseFloat(value);
                                                            if (value === '') {
                                                                handleHoursChange(projectId, date, 0);
                                                            } else if (!isNaN(numValue) && numValue >= 0 && numValue <= 24) {
                                                                handleHoursChange(projectId, date, numValue);
                                                            }
                                                        }}
                                                        min={0}
                                                        max={24}
                                                    />
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
                            <td>Grand Total</td>
                            {weekDates.map(date => {
                                const dayTotal = projectEntries.reduce((sum, projectId) => {
                                    const entry = getEntryForCell(projectId, date);
                                    return sum + (entry?.amount || 0);
                                }, 0);
                                return <td key={date}>{dayTotal || ''}</td>;
                            })}
                            <td>
                                {projectEntries.reduce((sum, projectId) => {
                                    return sum + weekDates.reduce((daySum, date) => {
                                        const entry = getEntryForCell(projectId, date);
                                        return daySum + (entry?.amount || 0);
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
import React, { useState, useEffect } from 'react';
import { Project, TimeEntry } from '../types/types';
import axios from 'axios';
import { jwtDecode } from "jwt-decode";
import { CircularProgress, TablePagination, Paper } from '@mui/material';
import './ReviewTimeEntries.css';

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

const getTokenFromCookie = (): string | null => {
    const token = document.cookie
        .split('; ')
        .find(row => row.startsWith('auth_token='))
        ?.split('=')[1];
    return token || null;
};

const ReviewTimeEntries: React.FC = () => {
    const [selectedEmployee, setSelectedEmployee] = useState<string>('all');
    const [selectedProject, setSelectedProject] = useState<string>('all');
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [projects, setProjects] = useState<Project[]>([]);
    const [timeEntries, setTimeEntries] = useState<TimeEntry[]>([]);
    const [loading, setLoading] = useState(true);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);

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

    const fetchTimeEntries = async () => {
        try {
            const token = getTokenFromCookie();
            if (!token) {
                throw new Error('No authentication token found');
            }

            const decodedToken = jwtDecode<DecodedToken>(token);
            const companyId = decodedToken.CompanyId;

            // Build query parameters based on selected filters
            const queryParams = new URLSearchParams();
            if (selectedEmployee !== 'all') {
                queryParams.append('personnelId', selectedEmployee);
            }
            if (selectedProject !== 'all') {
                queryParams.append('projectId', selectedProject);
            }

            const response = await axios.get<TimeEntry[]>(
                `https://localhost:7076/api/v1/company/${companyId}/time-entries${queryParams.toString() ? `?${queryParams.toString()}` : ''}`,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                }
            );

            setTimeEntries(response.data);
            setPage(0); // Reset to first page when new data is fetched
        } catch (error) {
            console.error('Error fetching time entries:', error);
        }
    };

    useEffect(() => {
        Promise.all([fetchEmployees(), fetchProjects()]);
    }, []);

    useEffect(() => {
        if (!loading) {
            fetchTimeEntries();
        }
    }, [selectedEmployee, selectedProject, loading]);

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    if (loading) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                <CircularProgress />
            </div>
        );
    }

    // Calculate the current page's data
    const currentPageData = timeEntries.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage);

    return (
        <div className="review-time-entries-container">
            <div className="filters-container">
                <div className="filter">
                    <label>Employee:</label>
                    <select
                        value={selectedEmployee}
                        onChange={(e) => setSelectedEmployee(e.target.value)}
                        className="filter-select"
                    >
                        <option value="all">All Employees</option>
                        {employees.map(emp => (
                            <option key={emp.personnelId} value={emp.personnelId}>
                                {`${emp.firstName} ${emp.lastName}`}
                            </option>
                        ))}
                    </select>
                </div>

                <div className="filter">
                    <label>Project:</label>
                    <select
                        value={selectedProject}
                        onChange={(e) => setSelectedProject(e.target.value)}
                        className="filter-select"
                    >
                        <option value="all">All Projects</option>
                        {projects.map(project => (
                            <option key={project.projectId} value={project.projectId}>
                                {project.projectName}
                            </option>
                        ))}
                    </select>
                </div>
            </div>

            <Paper className="time-entries-table">
                <table>
                    <thead>
                        <tr>
                            <th>Employee</th>
                            <th>Project</th>
                            <th>Date</th>
                            <th>Hours</th>
                        </tr>
                    </thead>
                    <tbody>
                        {currentPageData.map(entry => {
                            const employee = employees.find(emp => emp.personnelId === entry.personnelId);
                            const project = projects.find(proj => proj.projectId === entry.projectId);
                            
                            return (
                                <tr key={entry.timeEntriesId}>
                                    <td>{employee ? `${employee.firstName} ${employee.lastName}` : 'Unknown'}</td>
                                    <td>{project ? project.projectName : 'Unknown'}</td>
                                    <td>{new Date(entry.timeEntriesDate).toLocaleDateString()}</td>
                                    <td>{entry.amount}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
                <TablePagination
                    rowsPerPageOptions={[10, 25, 50]}
                    component="div"
                    count={timeEntries.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onPageChange={handleChangePage}
                    onRowsPerPageChange={handleChangeRowsPerPage}
                />
            </Paper>
        </div>
    );
};

export default ReviewTimeEntries; 
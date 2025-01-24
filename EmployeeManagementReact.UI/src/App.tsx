import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Layout from './components/Layout';
import ProtectedRoute from './components/ProtectedRoute';
import Login from './pages/Login';
import Unauthorized from './pages/Unauthorized';
import Dashboard from './pages/Dashboard';
import Company from './pages/Company';
import Pages from './pages/Pages';
import EmployeeManagement from './pages/EmployeeManagement';
import Projects from './pages/Projects';
import TimeEntries from './pages/TimeEntries';

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route path="/unauthorized" element={<Unauthorized />} />
                
                <Route element={<ProtectedRoute />}>
                    <Route path="/" element={<Layout />}>
                        <Route index element={<Navigate to="/dashboard" replace />} />
                        <Route path="dashboard" element={<Dashboard />} />
                        <Route path="company" element={<Company />} />
                        <Route path="pages" element={<Pages />} />
                        <Route path="employee" element={<EmployeeManagement />} />
                        <Route path="projects" element={<Projects />} />
                        <Route path="time-entries" element={<TimeEntries />} />
                    </Route>
                </Route>
            </Routes>
        </Router>
    );
};

export default App; 
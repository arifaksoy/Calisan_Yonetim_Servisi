import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { getAuthToken } from '../services/authService';

const ProtectedRoute: React.FC = () => {
    const token = getAuthToken();

    if (!token) {
        return <Navigate to="/unauthorized" replace />;
    }

    return <Outlet />;
};

export default ProtectedRoute; 
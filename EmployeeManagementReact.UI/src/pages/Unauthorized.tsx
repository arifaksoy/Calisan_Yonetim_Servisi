import React from 'react';
import { Link } from 'react-router-dom';
import './Unauthorized.css';

const Unauthorized: React.FC = () => {
    return (
        <div className="unauthorized-container">
            <div className="unauthorized-box">
                <h1>Access Denied</h1>
                <p>You are not authorized to access this page.</p>
                <Link to="/login" className="login-link">Go to Login</Link>
            </div>
        </div>
    );
};

export default Unauthorized; 
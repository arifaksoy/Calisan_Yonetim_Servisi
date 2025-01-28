import React, { useEffect, useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { MenuItem } from '../types/types';
import { getTokenClaims, removeAuthToken } from '../services/authService';
import './Sidebar.css';

interface SidebarProps {
    menuItems: MenuItem[];
}

const Sidebar: React.FC<SidebarProps> = ({ menuItems }) => {
    const location = useLocation();
    const navigate = useNavigate();
    const [userData, setUserData] = useState({ username: '', companyName: '' });

    useEffect(() => {
        const claims = getTokenClaims();
        if (claims) {
            setUserData({
                username: claims.sub || '',
                companyName: claims.CompanyName || ''
            });
        }
    }, []);

    const handleLogout = () => {
        removeAuthToken();
        navigate('/login');
    };

    return (
        <div className="sidebar">
            <div className="sidebar-header">
                <img 
                    src={`https://api.dicebear.com/7.x/initials/svg?seed=${userData.username}`}
                    alt="User Avatar" 
                    className="avatar" 
                />
                <div className="user-info">
                    <span className="company-name">{userData.companyName}</span>
                    <span className="username">{userData.username}</span>
                </div>
            </div>
            <nav className="sidebar-nav">
                {menuItems.map((item) => (
                    <Link
                        key={item.id}
                        to={item.path}
                        className={`nav-item ${location.pathname === item.path ? 'active' : ''}`}
                    >
                        <span className="material-icons">{item.icon}</span>
                        <span>{item.title}</span>
                    </Link>
                ))}
                <button onClick={handleLogout} className="nav-item logout-button">
                    <span className="material-icons">logout</span>
                    <span>Logout</span>
                </button>
            </nav>
        </div>
    );
};

export default Sidebar; 
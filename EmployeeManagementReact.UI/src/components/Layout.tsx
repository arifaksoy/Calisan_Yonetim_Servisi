import React, { useEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';
import { getMyPages } from '../services/pageService';
import type { Page } from '../services/pageService';
import './Layout.css';

// Default icons mapping
const defaultIcons: { [key: string]: string } = {
    'Dashboard': 'dashboard',
    'Company': 'business',
    'Pages': 'menu_book',
    'Employee': 'people',
    'Project': 'assignment'
};

// URL mapping for pages
const urlMapping: { [key: string]: string } = {
    'Dashboard': '/dashboard',
    'Company': '/company',
    'Pages': '/pages',
    'Employee': '/employee',
    'Project': '/projects'
};

// Dashboard menu item that will always be present
const dashboardMenuItem = {
    id: 'dashboard',
    title: 'Dashboard',
    path: '/dashboard',
    icon: 'dashboard'
};

const Layout: React.FC = () => {
    const [menuItems, setMenuItems] = useState<Array<{
        id: string;
        title: string;
        path: string;
        icon: string;
    }>>([dashboardMenuItem]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchPages = async () => {
            try {
                const pages = await getMyPages();
                console.log('Fetched pages:', pages);
                
                if (Array.isArray(pages)) {
                    const formattedMenuItems = pages
                        .filter(page => page.status === 1)
                        .map(page => ({
                            id: page.pageId,
                            title: page.pageName,
                            path: urlMapping[page.pageName] || `/${page.pageName.toLowerCase().replace(/\s+/g, '-')}`,
                            icon: defaultIcons[page.pageName] || 'article'
                        }));
                    console.log('Formatted menu items:', formattedMenuItems);
                    // Combine dashboard with other menu items
                    setMenuItems([dashboardMenuItem, ...formattedMenuItems]);
                } else {
                    setError('Invalid data format received from server');
                }
            } catch (error) {
                console.error('Failed to fetch pages:', error);
                setError('Failed to load menu items');
            }
        };

        fetchPages();
    }, []);

    if (error) {
        console.error('Error in Layout:', error);
    }

    return (
        <div className="layout">
            <Sidebar menuItems={menuItems} />
            <div className="main-content">
                <Outlet />
            </div>
        </div>
    );
};

export default Layout; 
import React, { useEffect, useState } from 'react';
import { getMyPages } from '../services/pageService';

const Dashboard: React.FC = () => {
    const [pageTitle, setPageTitle] = useState('Dashboard');

    useEffect(() => {
        const fetchPageTitle = async () => {
            try {
                const pages = await getMyPages();
                const dashboardPage = pages.find(page => page.url.includes('dashboard'));
                if (dashboardPage) {
                    setPageTitle(dashboardPage.pageName);
                }
            } catch (error) {
                console.error('Error fetching page title:', error);
            }
        };

        fetchPageTitle();
    }, []);

    return (
        <div className="page-container">
            <h1 className="page-title">{pageTitle}</h1>
            {/* Dashboard content will go here */}
        </div>
    );
};

export default Dashboard; 
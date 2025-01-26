import { MenuItem, Employee, Project, TimeEntry } from '../types/types';

export const mockMenuItems: MenuItem[] = [
    {
        id: '1',
        title: 'Dashboard',
        path: '/dashboard',
        icon: 'dashboard'
    },
    {
        id: '2',
        title: 'Employee Management',
        path: '/employee-management',
        icon: 'people'
    },
    {
        id: '3',
        title: 'System Management',
        path: '/system-management',
        icon: 'settings'
    },
    {
        id: '4',
        title: 'User Management',
        path: '/user-management',
        icon: 'person'
    }
];

export const mockEmployees: Employee[] = [
    { id: 1, name: 'Arif Aksoy' },
    { id: 2, name: 'John Doe' },
    { id: 3, name: 'Jane Smith' }
];

export const mockProjects: Project[] = [
    { 
        projectId: '22CS001.01',
        projectName: 'Verforce - Development',
        projectDescription: 'Verforce Development Project',
        companyId: '1',
        companyName: 'Default Company',
        status: 1
    },
    { 
        projectId: '22CS002.01',
        projectName: 'CRM Integration',
        projectDescription: 'CRM Integration Project',
        companyId: '1',
        companyName: 'Default Company',
        status: 1
    },
    { 
        projectId: '22CS003.01',
        projectName: 'Mobile App Development',
        projectDescription: 'Mobile App Development Project',
        companyId: '1',
        companyName: 'Default Company',
        status: 1
    }
];

export const mockTimeEntries: TimeEntry[] = [
    {
        timeEntriesId: "1",
        timeEntriesDate: "2024-01-20T00:00:00",
        projectId: '22CS001.01',
        projectName: 'Verforce - Development',
        personnelId: "1",
        personnelName: "Arif Aksoy",
        amount: 8.5,
        status: 1
    },
    {
        timeEntriesId: "2",
        timeEntriesDate: "2024-01-20T00:00:00",
        projectId: '22CS002.01',
        projectName: 'CRM Integration',
        personnelId: "1",
        personnelName: "Arif Aksoy",
        amount: 4,
        status: 1
    }
]; 
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
    { id: '22CS001.01', name: 'Verforce - Development' },
    { id: '22CS002.01', name: 'CRM Integration' },
    { id: '22CS003.01', name: 'Mobile App Development' }
];

export const mockTimeEntries: TimeEntry[] = [
    {
        id: 1,
        employeeId: 1,
        projectId: '22CS001.01',
        date: '2024-01-20',
        hours: 8.5,
        comment: 'Frontend development'
    },
    {
        id: 2,
        employeeId: 1,
        projectId: '22CS002.01',
        date: '2024-01-20',
        hours: 4,
        comment: 'API integration'
    }
]; 
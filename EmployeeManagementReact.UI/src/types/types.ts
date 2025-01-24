export interface MenuItem {
    id: string;
    title: string;
    path: string;
    icon?: string;
    children?: MenuItem[];
}

export interface User {
    name: string;
    avatar: string;
}

export interface Employee {
    id: number;
    name: string;
}

export interface Project {
    id: string;
    name: string;
}

export interface TimeEntry {
    id: number;
    employeeId: number;
    projectId: string;
    date: string;
    hours: number;
    comment?: string;
}

export interface WeeklyTimesheet {
    startDate: string;
    endDate: string;
    entries: TimeEntry[];
} 
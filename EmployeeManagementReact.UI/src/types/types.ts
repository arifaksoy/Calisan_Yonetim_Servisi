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
    projectId: string;
    projectName: string;
    projectDescription: string;
    companyId: string;
    companyName: string;
    status: number;
}

export interface TimeEntry {
    timeEntriesId: string;
    timeEntriesDate: string;
    projectId: string;
    projectName: string;
    personnelId: string;
    personnelName: string;
    amount: number | null;
    status: number;
}

export interface WeeklyTimesheet {
    startDate: string;
    endDate: string;
    entries: TimeEntry[];
} 
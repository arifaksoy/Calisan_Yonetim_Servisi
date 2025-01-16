export interface LoginCredentials {
    username: string;
    password: string;
}

export interface LoginResponse {
    token: string;
}

export interface AuthState {
    isAuthenticated: boolean;
    token: string | null;
} 
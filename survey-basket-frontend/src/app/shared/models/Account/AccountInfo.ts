// User Account

export interface AccountInfo {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    isEmailConfirmed: boolean;
    roles: string[];
}

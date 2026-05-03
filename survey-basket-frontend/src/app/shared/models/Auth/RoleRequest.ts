// Roles

export interface RoleRequest {
    name: string;
    isDefault: boolean;
    permissions: string[];
}

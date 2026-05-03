// Users Admin

import { AbstractControl, ValidationErrors } from "@angular/forms";

export interface UserCreateRequest {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    roles: string[];
}

export function passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirm = control.get('confirmPassword')?.value;
    return password && confirm && password !== confirm ? { passwordMismatch: true } : null;
}


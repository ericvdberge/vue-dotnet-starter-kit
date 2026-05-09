import type { Role } from "./roles";

export interface User {
    id: number;
    name: string;
    description: string;
    roles?: Role[];
}
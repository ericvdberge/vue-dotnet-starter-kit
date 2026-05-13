import { useQuery } from "@tanstack/vue-query";
import type { Role } from "../types/roles"

export const useRoles = () => {

    const getRoles = () => {
        return useQuery({
            queryKey: ['roles'],
            queryFn: async () => {
                return await Promise.resolve(buildRoles());
            }
        })
    }
    
    return { getRoles };
}

function buildRoles(): Role[] {
    return [
        { id: 1, name: 'Admin', description: 'Administrator role with full permissions' },
        { id: 2, name: 'Editor', description: 'Editor role with permissions to edit content' },
        { id: 3, name: 'Viewer', description: 'Viewer role with read-only permissions' },
    ]
}
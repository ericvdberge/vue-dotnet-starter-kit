import { useMutation, useQuery, useQueryClient } from "@tanstack/vue-query"
import { computed, type Ref } from "vue"
import type { Role } from "../types/roles";
import type { Permission } from "../types/permissions";

export const usePermissions = (role: Ref<Role | null>) => {  
    const queryClient = useQueryClient();

    const getPermissions = () => {
      return useQuery({
        queryKey: computed(() => ['permissions', role.value?.id]),
        queryFn: async () => {
            return buildPermissions(role.value?.id);
        }
      })
    }

    const togglePermission = useMutation({
        mutationFn: async ({ group, permissionId }: { permissionId: number, group: string }) => {
            return Promise.resolve({ group, permissionId }); //TODO: Replace with actual API call
        },
        onMutate: async ({ group, permissionId }) => {
            await queryClient.cancelQueries({
                queryKey: ['permissions', role.value?.id]
            });

            queryClient.setQueryData(['permissions', role.value?.id], (old: any) => {
                if (!old) return old 
                const updatedData = {
                    ...old,
                    [group]: old[group].map((perm: Permission) =>
                        perm.id === permissionId
                        ? { ...perm, enabled: !perm.enabled }
                        : perm
                    ),
                };
                return updatedData;
            })
        }
    })

    return {
        getPermissions,
        togglePermission
    }
}


function buildPermissions(roleId?: number): { [key: string]: Permission[] } {
  return {
    "User Management": [
      {
        id: 1,
        name: "Create User",
        description: "Allows creating new users",
        enabled: roleId === 1,
      },
      {
        id: 2,
        name: "Edit User",
        description: "Allows editing existing users",
        enabled: roleId === 1 || roleId === 2,
      },
      {
        id: 3,
        name: "Delete User",
        description: "Allows deleting users",
        enabled: roleId === 1,
      },
      {
        id: 4,
        name: "View User",
        description: "Allows viewing user information",
        enabled: true,
      },
    ],
    "Role Management": [
      {
        id: 5,
        name: "Create Role",
        description: "Allows creating new roles",
        enabled: roleId === 1,
      },
      {
        id: 6,
        name: "Edit Role",
        description: "Allows editing existing roles",
        enabled: roleId === 1 || roleId === 2,
      },
      {
        id: 7,
        name: "Delete Role",
        description: "Allows deleting roles",
        enabled: roleId === 1,
      },
      {
        id: 8,
        name: "View Role",
        description: "Allows viewing role information",
        enabled: true,
      },
    ]
  }
}
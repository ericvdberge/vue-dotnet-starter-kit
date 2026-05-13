<script setup lang="ts">
import { useRolesTable } from '../../composables/useRolesTable'
import type { Role } from '../../types/roles';
import RolePermissionsDrawer from './RolePermissionsDrawer.vue';
import { computed, ref } from 'vue';
import { usePermissions } from '../../composables/usePermissions';
import { useRoles } from '../../composables/useRoles';
import RoleFilters from './RoleFilters.vue';
import VirtualTable from '@/components/VirtualTable.vue';

/**
 * Get the roles that are displayed in the virtual table
 */
const { getRoles } = useRoles();
const { data: roles } = getRoles();
const { table } = useRolesTable(roles);


/**
 * The permissions that are displayed in the drawer
 * are fetched based on the selected role in the table. 
 * When a row is clicked, the selected role is updated,
 *  which triggers the permissions query to fetch the permissions 
 * for that role and display them in the drawer.
 */
const selectedRole = ref<Role | null>(null);
const openDrawer = ref(false);
const { getPermissions } = usePermissions(selectedRole);
const { data: permissionGroups } = getPermissions();

const handleRowClick = (row: Role) => {
    selectedRole.value = row;
    openDrawer.value = !openDrawer.value;
};
</script>

<template>
    <h1 class="text-lg font-bold mt-6">Roles</h1>

    <RoleFilters :roles="roles" :table="table" />

    <VirtualTable
        :table="table"
        class="mt-5 h-75 w-full" 
        @row-click="handleRowClick"
    />

    <RolePermissionsDrawer 
        :open="openDrawer" 
        :role="selectedRole"
        :permissionGroups="permissionGroups"
        @update:open="openDrawer = $event" 
    />
</template>
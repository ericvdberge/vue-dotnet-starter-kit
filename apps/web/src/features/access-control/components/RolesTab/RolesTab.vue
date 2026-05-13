<script setup lang="ts">
import { useRolesTable } from '../../composables/useRolesTable'
import type { Role } from '../../types/roles';
import RolePermissionsDrawer from './RolePermissionsDrawer.vue';
import { computed, ref } from 'vue';
import { usePermissions } from '../../composables/usePermissions';
import { useRoles } from '../../composables/useRoles';
import RoleFilters from './RoleFilters.vue';
import VirtualTable from '@/components/VirtualTable.vue';

const selectedRole = ref<Role | null>(null);
const { permissionsQuery } = usePermissions(selectedRole);
const permissionGroups = computed(() => permissionsQuery?.data.value || {});

const { rolesQuery } = useRoles();
const roles = computed(() => rolesQuery.data.value ?? []);
const { table } = useRolesTable(roles);
const openDrawer = ref(false);

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
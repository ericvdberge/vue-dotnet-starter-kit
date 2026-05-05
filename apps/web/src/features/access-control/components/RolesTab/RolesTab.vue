<script setup lang="ts">
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';
import { useRolesTable } from '../../composables/useRolesTable'
import { FlexRender, type Row } from '@tanstack/vue-table';
import type { Role } from '../../types/roles';
import RolePermissionsDrawer from './RolePermissionsDrawer.vue';
import { computed, ref } from 'vue';
import { usePermissions } from '../../composables/usePermissions';
import type { AcceptableValue } from 'reka-ui';
import { useRoles } from '../../composables/useRoles';
import RoleFilters from './RoleFilters.vue';

const selectedRole = ref<Role | null>(null);
const { permissionsQuery } = usePermissions(selectedRole);
const permissionGroups = computed(() => permissionsQuery?.data.value || {});

const { rolesQuery } = useRoles();
const roles = computed(() => rolesQuery.data.value ?? []);
const { table } = useRolesTable(roles);
const openDrawer = ref(false);

const handleRowClick = (row: Row<Role>) => {
    selectedRole.value = row.original
    openDrawer.value = !openDrawer.value;
};


</script>

<template>
    <h1 class="text-lg font-bold mt-6">Roles</h1>

    <RoleFilters :roles="roles" :table="table" />

    <Table class="mt-5">
        <TableHeader>
            <TableRow v-for="headerGroup in table.getHeaderGroups()">
                <TableHead v-for="header in headerGroup.headers">
                    {{ header.column.columnDef.header }}
                </TableHead>
            </TableRow>
        </TableHeader>

        <TableBody> 
            <TableRow v-for="row in table.getFilteredRowModel().rows" @click="handleRowClick(row)" :key="row.id">
                <TableCell v-for="cell in row.getVisibleCells()">
                    <FlexRender
                        :render="cell.column.columnDef.cell"
                        :props="cell.getContext()"
                    />
                </TableCell>
            </TableRow>
        </TableBody>
    </Table>

    <RolePermissionsDrawer 
        :open="openDrawer" 
        :role="selectedRole"
        :permissionGroups="permissionGroups"
        @update:open="openDrawer = $event" 
    />
</template>
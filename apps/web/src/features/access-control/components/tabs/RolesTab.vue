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
import RolePermissionsDrawer from '../RolePermissionsDrawer.vue';
import { ref } from 'vue';

const { table } = useRolesTable()
const openDrawer = ref(false);

const handleRowClick = (row: Row<Role>) => {
    // You can access the role data using row.original
    const role = row.original;
    console.log('Clicked role:', role);

    // Open the permissions drawer
    openDrawer.value = !openDrawer.value;
};
</script>

<template>
    <h1 class="text-2xl font-bold mt-6">Roles</h1>

    <Table class="mt-5">
        <TableHeader>
            <TableRow v-for="headerGroup in table.getHeaderGroups()">
                <TableHead v-for="header in headerGroup.headers">
                    {{ header.column.columnDef.header }}
                </TableHead>
            </TableRow>
        </TableHeader>

        <TableBody> 
            <TableRow v-for="row in table.getRowModel().rows" @click="handleRowClick(row)">
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
        @update:open="openDrawer = $event" 
    />
</template>
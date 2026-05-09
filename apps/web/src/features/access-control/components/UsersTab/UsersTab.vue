<script setup lang="ts">
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/components/ui/table';

import { FlexRender } from '@tanstack/vue-table';
import { computed, useTemplateRef, watch } from 'vue';
import { useUsers } from '../../composables/useUsers';
import { useUsersTable } from '../../composables/useUsersTable';
import { useVirtualizer } from '@tanstack/vue-virtual';

const { usersQuery } = useUsers();
const users = computed(() => usersQuery.data?.value || []);
const { table } = useUsersTable(users);
const rows = computed(() => table.getRowModel().rows);
const parentRef = useTemplateRef<HTMLElement>('parentRef');

const virtualRows = useVirtualizer({
    get count() {
        return rows.value.length
    },
    getScrollElement: () => parentRef.value,
    estimateSize: () => 40,
    overscan: 10,
});

const virtualItems = computed(() =>
    virtualRows.value.getVirtualItems()
);

const totalSize = computed(() => table.getTotalSize());
</script>

<template>
    <h1 class="text-lg font-bold mt-6">Users</h1>

    <!-- SCROLL CONTAINER -->
    <div
        ref="parentRef"
        class="mt-5 max-h-112.5 overflow-auto w-full"
    >
        <Table :style="{width: `${totalSize}px`}">
            <TableHeader>
                <TableRow
                    v-for="headerGroup in table.getHeaderGroups()"
                    :key="headerGroup.id"
                    class="w-full"
                >
                    <TableHead
                        v-for="header in headerGroup.headers"
                        :key="header.id"
                        :style="{
                            minWidth: `${header.column.getSize()}px`,
                            width: `${header.column.getSize()}px`,
                        }"
                    >
                        {{ header.column.columnDef.header }}
                    </TableHead>
                </TableRow>
            </TableHeader>

            <TableBody
                :style="{
                    height: `${virtualRows.getTotalSize()}px`,
                    position: 'relative',
                }"
                >
                <TableRow
                    v-for="row in virtualItems"
                    :key="row.index"
                    class="w-full"
                    :style="{
                        position: 'absolute',
                        top: 0,
                        left: 0,
                        transform: `translateY(${row.start}px)`,
                    }"
                >
                    <TableCell
                        v-for="cell in rows[row.index]?.getVisibleCells()"
                        :key="cell.id"
                        :style="{
                            minWidth: `${cell.column.getSize()}px`,
                            width: `${cell.column.getSize()}px`,
                        }"
                    >
                        <FlexRender
                            :render="cell.column.columnDef.cell"
                            :props="cell.getContext()"
                        />
                    </TableCell>
                </TableRow>
            </TableBody>
        </Table>
    </div>
</template>
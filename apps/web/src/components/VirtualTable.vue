<!-- VirtualTable.vue -->
<script setup lang="ts" generic="TData">
import { computed, ref } from 'vue';
import { FlexRender, type Table as TanstackTable} from '@tanstack/vue-table';
import { useVirtualizer } from '@tanstack/vue-virtual';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/components/ui/table';
import NoResultsTableRow from './NoResultsTableRow.vue';

interface Props {
    table: TanstackTable<TData>;
    height?: number;
    estimateRowHeight?: number;
    overscan?: number;
    minLastColumnWidth?: number;
}

interface Emits {
    'row-click': [row: TData]
}

const { table, height = 600, estimateRowHeight = 36, overscan = 10, minLastColumnWidth = 200 } = defineProps<Props>();
const emit = defineEmits<Emits>();

const parentRef = ref<HTMLElement | null>(null);
const rows = computed(() => table.getRowModel().rows);
const columns = computed(() =>
    table.getVisibleLeafColumns()
);

const virtualizer = useVirtualizer({
    get count() {
        return rows.value.length;
    },
    getScrollElement: () => parentRef.value,
    estimateSize: () => estimateRowHeight,
    overscan,
});

const virtualRows = computed(() =>
    virtualizer.value.getVirtualItems()
);

const totalFixedWidth = computed(() => {
    return columns.value
        .slice(0, -1)
        .reduce((acc, column) => {
            return acc + column.getSize();
        }, 0);
});

const lastColumnWidth = computed(() => {
    const containerWidth =
        parentRef.value?.clientWidth ?? 0;

    return Math.max(
        minLastColumnWidth,
        containerWidth - totalFixedWidth.value
    );
});

const getColumnStyle = (index: number, size: number) => {
    const isLastColumn =
        index === columns.value.length - 1;

    if (isLastColumn) {
        return {
            flex: `1 1 ${lastColumnWidth.value}px`,
            width: `${lastColumnWidth.value}px`,
            minWidth: `${lastColumnWidth.value}px`,
        };
    }

    return {
        flex: `0 0 ${size}px`,
        width: `${size}px`,
        minWidth: `${size}px`,
    };
}

const handleRowClick = (row: TData | undefined): void => {
    if (!row) return;
    emit('row-click', row);
};
</script>

<template>
    <div class="overflow-hidden rounded-md bg-background">
        <!-- HEADER -->
        <div class="sticky top-0 z-20 bg-background">
            <Table class="w-max min-w-full">
                <TableHeader>
                    <TableRow v-for="headerGroup in table.getHeaderGroups()" class="flex w-full" :key="headerGroup.id">
                        <TableHead
                            v-for="(header, index) in headerGroup.headers"
                            :key="header.id"
                            class="flex items-center"
                            :style="getColumnStyle(index, header.column.getSize())"
                        >
                            <FlexRender
                                v-if="!header.isPlaceholder"
                                :render="header.column.columnDef.header"
                                :props="header.getContext()"
                            />
                        </TableHead>
                    </TableRow>
                </TableHeader>
            </Table>
        </div>

        <!-- BODY -->
        <div
            ref="parentRef"
            :class="`overflow-auto`"
            :style="{height: `${height}px`}"
        >
            <NoResultsTableRow v-if="rows.length === 0" />

            <Table class="w-max min-w-full">
                <TableBody
                    class="relative block"
                    :style="{ height: `${virtualizer.getTotalSize()}px` }"
                >
                    <TableRow
                        v-for="virtualRow in virtualRows"
                        :key="virtualRow.index"
                        class="absolute left-0 top-0 flex w-full"
                        :style="{
                            transform: `translateY(${virtualRow.start}px)`,
                        }"
                        @click="handleRowClick(rows[virtualRow.index]?.original)"
                    >
                        <TableCell
                            v-for="(cell, index) in rows[virtualRow.index]?.getVisibleCells()"
                            :key="cell.id"
                            class="flex items-center"
                            :style="getColumnStyle(index, cell.column.getSize())"
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
    </div>
</template>
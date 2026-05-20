<!-- VirtualTable.vue -->
<script setup lang="ts" generic="TData">
import { computed, onMounted, ref, watch } from 'vue';
import { FlexRender, type Table as TanstackTable } from '@tanstack/vue-table';
import { useVirtualizer } from '@tanstack/vue-virtual';
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow
} from '@/components/ui/table';

import NoResultsTableRow from './NoResultsTableRow.vue';

interface Props {
  table: TanstackTable<TData>;
  height?: number;
  estimateRowHeight?: number;
  overscan?: number;
  minLastColumnWidth?: number;
}

interface Emits {
  (e: 'row-click', row: TData): void;
  (e: 'reach-end'): void;
  (e: 'reach-start'): void;
}

const props = defineProps<Props>();
const emit = defineEmits<Emits>();

const {
  table,
  height = 600,
  estimateRowHeight = 36,
  overscan = 10,
  minLastColumnWidth = 200
} = props;

const parentRef = ref<HTMLElement | null>(null);

const rows = computed(() => table.getRowModel().rows);
const columns = computed(() => table.getVisibleLeafColumns());

/**
 * Virtualizer
 */
const virtualizer = useVirtualizer({
  get count() {
    return rows.value.length;
  },
  getScrollElement: () => parentRef.value,
  estimateSize: () => estimateRowHeight,
  overscan,
  getItemKey: index => rows.value[index]?.id ?? index,
});

const virtualRows = computed(() =>
  virtualizer.value.getVirtualItems()
);

let lastEmitTime = 0;
const EMIT_COOLDOWN_MS = 300;

onMounted(() => {
  if (!parentRef.value) return

  const handleScroll = () => {
    const scrollElement = parentRef.value
    if (!scrollElement) return

    const lastItem = virtualRows.value[virtualRows.value.length - 1]
    if (!lastItem) return

    const buffer = 20
    const shouldLoadMore = lastItem.index >= rows.value.length - buffer

    const now = Date.now()
    const canEmit = now - lastEmitTime > EMIT_COOLDOWN_MS

    if (shouldLoadMore && canEmit) {
      lastEmitTime = now
      emit('reach-end')
    }
  }

  parentRef.value.addEventListener('scroll', handleScroll)

  return () => {
    parentRef.value?.removeEventListener('scroll', handleScroll)
  }
})

/**
 * Column sizing
 */
const totalFixedWidth = computed(() =>
  columns.value.slice(0, -1).reduce((acc, column) => {
    return acc + column.getSize();
  }, 0)
);

const lastColumnWidth = computed(() => {
  const containerWidth = parentRef.value?.clientWidth ?? 0;
  return Math.max(minLastColumnWidth, containerWidth - totalFixedWidth.value);
});

const getColumnStyle = (index: number, size: number) => {
  const isLastColumn = index === columns.value.length - 1;

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
};

/**
 * Row click
 */
const handleRowClick = (row: TData | undefined) => {
  if (!row) return;
  emit('row-click', row);
};

// watch(
//   virtualRows,
//   (vItems) => {
//     if (!vItems.length) return

//     const last = vItems[vItems.length - 1]
//     const first = vItems[0]

//     const total = rows.value.length

//     // console.log('VirtualTable: watch: ', {
//     //   firstIndex: first!.index,
//     //   lastIndex: last!.index,
//     //   total,
//     // })

//     /**
//      * Near bottom
//      */
//     if (last!.index >= total - 10) {
//       emit('reach-end')
//     }

//     /**
//      * Near top
//      */
//     if (first!.index <= 10) {
//       emit('reach-start')
//     }
//   },
//   { deep: true }
// )
</script>

<template>
  <div class="overflow-hidden rounded-md bg-background">

    <!-- HEADER -->
    <div class="sticky top-0 z-20 bg-background">
      <Table class="w-max min-w-full">
        <TableHeader>
          <TableRow
            v-for="headerGroup in table.getHeaderGroups()"
            :key="headerGroup.id"
            class="flex w-full"
          >
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
      class="overflow-auto"
      :style="{ height: `${height}px` }"
    >
      <NoResultsTableRow v-if="rows.length === 0" />

      <Table class="w-max min-w-full">
        <TableBody
          class="relative block"
          :style="{ height: `${virtualizer.getTotalSize()}px` }"
        >

          <!-- TOP SENTINEL -->
          <!-- <div ref="topSentinel" /> -->

          <!-- VIRTUAL ROWS -->
          <TableRow
            v-for="virtualRow in virtualRows"
            :key="virtualRow.index"
            class="absolute left-0 top-0 flex w-full"
            :style="{
              transform: `translateY(${virtualRow.start}px)`
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

          <!-- BOTTOM SENTINEL -->
          <!-- <div ref="bottomSentinel" /> -->

        </TableBody>
      </Table>
    </div>
  </div>
</template>
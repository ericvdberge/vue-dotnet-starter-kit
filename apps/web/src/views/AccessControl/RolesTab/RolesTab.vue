<script setup lang="ts">
import { ref, h } from 'vue'
import { Drawer, DrawerContent } from '@/components/ui/drawer'
import { Input } from '@/components/ui/input'
import { Badge } from '@/components/ui/badge'

import {
  Empty,
  EmptyHeader,
  EmptyTitle,
  EmptyDescription
} from '@/components/ui/empty'

import {
  useVueTable,
  getCoreRowModel,
  getFilteredRowModel,
  createColumnHelper,
  FlexRender
} from '@tanstack/vue-table'

import Table from '@/components/ui/table/Table.vue'
import TableHeader from '@/components/ui/table/TableHeader.vue'
import TableRow from '@/components/ui/table/TableRow.vue'
import TableHead from '@/components/ui/table/TableHead.vue'
import TableBody from '@/components/ui/table/TableBody.vue'
import TableCell from '@/components/ui/table/TableCell.vue'
import RolePermissionDrawer from './RolesPermissionsDrawer.vue'

export type Role = {
  id: number
  name: string
  userCount: number
  isActive: boolean
  permissions?: string[]
}

const roles = ref<Role[]>([
  {
    id: 1,
    name: 'Admin',
    userCount: 3,
    isActive: true,
    permissions: ['users.read', 'users.write', 'roles.manage']
  },
  {
    id: 2,
    name: 'Editor',
    userCount: 12,
    isActive: true,
    permissions: ['content.read', 'content.write']
  }
])
const search = ref('')
const drawerOpen = ref(false)
const selectedRole = ref<Role | null>(null)

const columnHelper = createColumnHelper<Role>()

const columns = [
  columnHelper.accessor('name', {
    header: 'Role',
    cell: r => r.getValue(),
    filterFn: 'includesString'
  }),
  columnHelper.accessor('userCount', {
    header: 'Users',
    cell: r => r.getValue()
  }),
  columnHelper.accessor('isActive', {
    header: 'Status',
    cell: r =>
      r.getValue()
        ? h(Badge, { variant: 'secondary' }, () => 'Active')
        : h(Badge, { variant: 'outline' }, () => 'Unused')
  })
]

const table = useVueTable({
  data: roles.value,
  columns,
  state: {
    get globalFilter() {
      return search.value
    }
  },
  onGlobalFilterChange: updater => {
    search.value =
      typeof updater === 'function' ? updater(search.value) : updater
  },
  getCoreRowModel: getCoreRowModel(),
  getFilteredRowModel: getFilteredRowModel()
})

function openRole(role: Role) {
  selectedRole.value = role
  drawerOpen.value = true
}
</script>

<template>
  <div class="space-y-3">

    <!-- header -->
    <div class="flex justify-between items-center">
      <div class="font-semibold">Roles</div>
      <Input v-model="search" placeholder="Search..." class="w-56" />
    </div>

    <!-- table -->
    <div class="border rounded-lg overflow-hidden">
      <Table class="w-full text-sm">
        <TableHeader class="bg-muted/50">
          <TableRow>
            <TableHead
              v-for="h in table.getHeaderGroups()[0]?.headers"
              :key="h.id"
              class="text-left px-3 py-2 text-xs text-muted-foreground"
            >
              <FlexRender :render="h.column.columnDef.header" :props="h.getContext()" />
            </TableHead>
          </TableRow>
        </TableHeader>

        <TableBody v-if="table.getRowModel().rows.length">
          <TableRow
            v-for="row in table.getRowModel().rows"
            :key="row.id"
            class="border-t hover:bg-muted/50 cursor-pointer"
            @click="openRole(row.original)"
          >
            <TableCell
              v-for="cell in row.getVisibleCells()"
              :key="cell.id"
              class="px-3 py-2"
            >
              <FlexRender :render="cell.column.columnDef.cell" :props="cell.getContext()" />
            </TableCell>
          </TableRow>
        </TableBody>

        <!-- empty -->
        <TableBody v-else>
          <TableRow>
            <TableCell colspan="3" class="text-center">
              <Empty class="gap-0">
                <EmptyHeader>
                  <EmptyTitle>No roles found</EmptyTitle>
                </EmptyHeader>
                <EmptyDescription>
                  Try adjusting your search or create a new role.
                </EmptyDescription>
              </Empty>
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </div>
  </div>

  <!-- drawer -->
  <RolePermissionDrawer
    v-model:open="drawerOpen"
    :role="selectedRole"
    @update:role="r => selectedRole = r"
  />
</template>
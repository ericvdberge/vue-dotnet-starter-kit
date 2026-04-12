<script setup lang="ts">
import { ref } from 'vue'

// inline shadcn imports
import { Card, CardContent } from '@/components/ui/card'
import {
  Table,
  TableHeader,
  TableBody,
  TableHead,
  TableRow,
  TableCell
} from '@/components/ui/table'
import { Badge } from '@/components/ui/badge'

// state
const userTable = ref([
  { name: 'John Doe', role: 'Admin', scope: 'Global' },
  { name: 'Jane Smith', role: 'Editor', scope: 'Project A' }
])

// helpers for styling
const roleVariant = (role: string) => {
  switch (role) {
    case 'Admin':
      return 'destructive'
    case 'Editor':
      return 'default'
    default:
      return 'secondary'
  }
}
</script>

<template>
  <div class="mt-6">
    <Card>
      <CardContent class="p-4">

        <div class="border rounded-lg overflow-hidden">
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>User</TableHead>
                <TableHead>Role</TableHead>
                <TableHead>Scope</TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              <TableRow
                v-for="u in userTable"
                :key="u.name"
              >
                <!-- User -->
                <TableCell class="font-medium text-left" >
                  {{ u.name }}
                </TableCell>

                <!-- Role -->
                <TableCell class="text-left">
                  <Badge :variant="roleVariant(u.role)">
                    {{ u.role }}
                  </Badge>
                </TableCell>

                <!-- Scope -->
                <TableCell class="text-left">
                  <Badge variant="secondary">
                    {{ u.scope }}
                  </Badge>
                </TableCell>

              </TableRow>
            </TableBody>
          </Table>
        </div>

      </CardContent>
    </Card>
  </div>
</template>
<script setup lang="ts">
import { computed, ref } from 'vue'

// inline shadcn imports
import { Input } from '@/components/ui/input'
import { Card, CardContent } from '@/components/ui/card'
import { Table, TableHeader, TableBody, TableHead, TableRow, TableCell } from '@/components/ui/table'
import { Badge } from '@/components/ui/badge'

// state
const searchText = ref('')

const permissions = ref([
  { key: 'page.create', desc: 'Create pages', group: 'Pages' },
  { key: 'page.edit', desc: 'Edit pages', group: 'Pages' },
  { key: 'media.upload', desc: 'Upload media', group: 'Media' },
  { key: 'template.apply', desc: 'Apply templates', group: 'Templates' }
])

const filteredPermissions = computed(() => {
  if (!searchText.value) return permissions.value

  const query = searchText.value.toLowerCase()

  return permissions.value.filter(p =>
    p.key.toLowerCase().includes(query) ||
    p.desc.toLowerCase().includes(query) ||
    p.group.toLowerCase().includes(query)
  )
})
</script>

<template>
  <div class="mt-6">
    <Card>
      <CardContent class="p-4">

        <!-- Search -->
        <Input
          v-model="searchText"
          placeholder="Search permissions..."
          class="mb-4"
        />

        <!-- Table -->
        <div class="border rounded-lg overflow-hidden">
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Key</TableHead>
                <TableHead>Description</TableHead>
                <TableHead>Group</TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              <TableRow
                v-for="p in filteredPermissions"
                :key="p.key"
              >
                <TableCell class="font-mono text-xs">
                  {{ p.key }}
                </TableCell>

                <TableCell>
                  {{ p.desc }}
                </TableCell>

                <TableCell>
                  <Badge variant="secondary">
                    {{ p.group }}
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
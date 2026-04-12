<script setup lang="ts">
import { ref } from 'vue'

// shadcn components (adjust paths to your setup)
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardDescription } from '@/components/ui/card'
import { Badge } from '@/components/ui/badge'
import { Table, TableHeader, TableBody, TableHead, TableRow, TableCell } from '@/components/ui/table'
import { Checkbox } from '@/components/ui/checkbox'
import { toast } from 'vue-sonner'
import RoleSelector from './RolesTabSelector.vue'

// state
const roles = ref(['Admin', 'Editor', 'Viewer'])
const selectedRole = ref('Editor')

const resources = ref(['Pages', 'Media', 'Templates'])
const users = ref(['John Doe', 'Jane Smith'])

const showSaved = () => {
  toast.success('Successfully saved the role!', { position: 'top-center' })
}
</script>

<template>
  <div class="grid grid-cols-12 gap-6 mt-6">

    <!-- Navigation -->
    <RoleSelector
      :roles="roles"
      :selectedRole="selectedRole"
      @update:selectedRole="selectedRole = $event"
    />

    <!-- Main Content -->
    <Card class="col-span-9">
      <CardHeader class="flex flex-row items-start justify-between">
        <div class="space-y-1">
          <Input
            v-model="selectedRole"
            class="text-lg font-semibold border-none px-0 focus-visible:ring-0"
          />
          <CardDescription>
            Edit permissions for this role
          </CardDescription>
        </div>

        <div class="flex gap-2">
          <Button variant="outline">Duplicate</Button>
          <Button variant="destructive">Delete</Button>
          <Button @click="showSaved">Save</Button>
        </div>
      </CardHeader>

      <CardContent class="space-y-6">

        <!-- Permissions -->
        <div>
          <h3 class="text-sm font-medium mb-3">Permissions</h3>

          <div class="border rounded-lg overflow-hidden">
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Resource</TableHead>
                  <TableHead>Create</TableHead>
                  <TableHead>Read</TableHead>
                  <TableHead>Update</TableHead>
                  <TableHead>Delete</TableHead>
                </TableRow>
              </TableHeader>

              <TableBody>
                <TableRow v-for="res in resources" :key="res">
                  <TableCell class="font-medium">{{ res }}</TableCell>

                  <TableCell
                    v-for="i in 4"
                    :key="i"
                    class="text-left"
                  >
                    <Checkbox />
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </div>
        </div>

        <!-- Users -->
        <div>
          <h3 class="text-sm font-medium mb-2">Assigned Users</h3>

          <div class="flex gap-2 flex-wrap items-center">
            <Badge
              v-for="user in users"
              :key="user"
              variant="secondary"
            >
              {{ user }}
            </Badge>

            <Button variant="outline" size="sm">
              + Assign
            </Button>
          </div>
        </div>

      </CardContent>
    </Card>
  </div>
</template>
<script setup lang="ts">
import { computed } from 'vue'
import { Drawer, DrawerContent, DrawerOverlay } from '@/components/ui/drawer'
import { Checkbox } from '@/components/ui/checkbox'
import { Button } from '@/components/ui/button'
import { Separator } from '@/components/ui/separator'
import type { Role } from './RolesTab.vue'

type Permission = {
  key: string
  label: string
}

const props = defineProps<{
  open: boolean
  role: Role | null
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'update:role', role: Role): void
}>()

/**
 * Permission groups (can be API-driven later)
 */
const permissionGroups: Record<string, Permission[]> = {
  Users: [
    { key: 'users.read', label: 'Read users' },
    { key: 'users.write', label: 'Edit users' },
    { key: 'users.delete', label: 'Delete users' }
  ],
  Content: [
    { key: 'content.read', label: 'Read content' },
    { key: 'content.write', label: 'Edit content' },
    { key: 'content.publish', label: 'Publish content' }
  ]
}

/**
 * 🔥 SINGLE SOURCE OF TRUTH (NO WATCH, NO LOCAL STATE)
 */
const permissions = computed<string[]>({
  get: () => props.role?.permissions ?? [],
  set: (val) => {
    if (!props.role) return

    emit('update:role', {
      ...props.role,
      permissions: val
    })
  }
})

/**
 * Permission toggle
 */
function togglePermission(key: string, checked: boolean) {
  const current = permissions.value

  if (checked) {
    if (!current.includes(key)) {
      permissions.value = [...current, key]
    }
  } else {
    permissions.value = current.filter(p => p !== key)
  }
}

/**
 * Check state
 */
function isChecked(key: string) {
  return permissions.value.includes(key)
}

/**
 * Group helpers
 */
function groupCount(perms: Permission[]) {
  return perms.filter(p => permissions.value.includes(p.key)).length
}

function isGroupChecked(perms: Permission[]) {
  return groupCount(perms) === perms.length
}

function isGroupIndeterminate(perms: Permission[]) {
  const count = groupCount(perms)
  return count > 0 && count < perms.length
}

/**
 * Group toggle
 */
function toggleGroup(perms: Permission[], checked: boolean) {
  const keys = perms.map(p => p.key)
  const current = permissions.value

  if (checked) {
    permissions.value = Array.from(new Set([...current, ...keys]))
  } else {
    permissions.value = current.filter(p => !keys.includes(p))
  }
}
</script>

<template>
  <Drawer :open="open" @update:open="emit('update:open', $event)" direction="right">

    <!-- overlay (light + modern) -->
    <DrawerOverlay
      class="bg-background/10 backdrop-blur-[1px]"
    />

    <!-- drawer -->
    <DrawerContent class="w-[380px] ml-auto flex flex-col bg-background/90 border-l">

      <!-- header -->
      <div class="p-6">
        <div class="text-xs text-muted-foreground uppercase tracking-wide">
          Role
        </div>
        <div class="text-lg font-semibold">
          {{ role?.name }}
        </div>
      </div>

      <Separator />

      <!-- body -->
      <div class="flex-1 overflow-y-auto p-6 space-y-6">

        <div
          v-for="(perms, group) in permissionGroups"
          :key="group"
          class="space-y-3"
        >
          <!-- group header -->
          <div class="flex items-center justify-between">
            <div class="text-sm font-medium">
              {{ group }}
            </div>

            <Checkbox
              :checked="isGroupChecked(perms)"
              :indeterminate="isGroupIndeterminate(perms)"
              @update:checked="val => toggleGroup(perms, !!val)"
              class="data-[state=checked]:bg-primary data-[state=checked]:border-primary"
            />
          </div>

          <!-- permissions -->
          <div class="space-y-1">
            <div
              v-for="perm in perms"
              :key="perm.key"
              class="flex items-center justify-between px-3 py-2 rounded-md cursor-pointer select-none transition hover:bg-muted/60"
              :class="isChecked(perm.key) ? 'bg-muted/40' : ''"
              @click="togglePermission(perm.key, !isChecked(perm.key))"
            >
              <span class="text-sm">
                {{ perm.label }}
              </span>

              <Checkbox
                :checked="isChecked(perm.key)"
                class="data-[state=checked]:bg-primary data-[state=checked]:border-primary data-[state=checked]:text-primary-foreground"
              />
            </div>
          </div>
        </div>

      </div>

      <Separator />

      <!-- footer -->
      <div class="p-4 flex justify-between items-center">
        <div class="text-xs text-muted-foreground">
          {{ permissions.length }} permissions
        </div>

        <Button size="sm" @click="emit('update:open', false)">
          Done
        </Button>
      </div>

    </DrawerContent>
  </Drawer>
</template>
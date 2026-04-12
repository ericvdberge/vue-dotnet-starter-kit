<script setup lang="ts">
import { Card, CardContent } from '@/components/ui/card'

/**
 * Props:
 * - roles: list of role names to display
 * - selectedRole: currently selected role
 */
interface Props {
    roles: string[],
    selectedRole: string
}

/**
 *  Emits:
 * - update:selectedRole: emitted when a role is selected, payload is the selected role name
 */
interface Emits {
  (e: "update:selectedRole", role: string): void
}

/**
 *  Handles role selection and emits the selected role to the parent component
 */
const { roles, selectedRole } = defineProps<Props>()
const emit = defineEmits<Emits>()
const setSelectedRole = (role: string): void => {
    emit('update:selectedRole', role)
}
</script>

<template>
    <Card class="col-span-3">
        <CardContent class="space-y-1">
            <div
                v-for="role in roles"
                :key="role"
                @click="setSelectedRole(role)"
                class="px-3 py-2 rounded-lg cursor-pointer text-sm transition"
                :class="selectedRole === role ? 'bg-muted font-medium' : 'hover:bg-muted/50'"
            >
                {{ role }}
            </div>
        </CardContent>
    </Card>
</template>
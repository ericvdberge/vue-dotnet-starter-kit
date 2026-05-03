<script setup lang="ts">
import {
  Drawer,
  DrawerContent,
  DrawerDescription,
  DrawerHeader,
  DrawerTitle,
} from '@/components/ui/drawer'

import { 
    Card, 
    CardContent, 
    CardHeader, 
    CardTitle 
} from '@/components/ui/card';

import { Checkbox } from '@/components/ui/checkbox';

import { ChevronsLeftIcon } from 'lucide-vue-next';
import type { Role } from '../types/roles';
import type { Permission } from '../types/permissions';
import Label from '@/components/ui/label/Label.vue';
import { toRefs } from 'vue';
import { usePermissions } from '../composables/usePermissions';

interface RolePermissionsDrawerProps {
  open: boolean,
  role: Role | null,
  permissionGroups: { [key: string]: Permission[] },
}

interface Emits {
    (e: 'update:open', open: boolean): void;
}

const props = defineProps<RolePermissionsDrawerProps>();
const { open, role, permissionGroups } = toRefs(props);
const emit = defineEmits<Emits>();

const handleClose = () => {
    emit('update:open', false);
};

const { togglePermission } = usePermissions(role);
</script>


<template>
    <Drawer :open="open" @update:open="emit('update:open', $event)" direction="right">
        <DrawerContent class="min-w-[70vw]">
            <DrawerHeader>
                <div class="flex align-center gap-3">
                    <ChevronsLeftIcon class="size-5" @click="handleClose" /> 
                    <DrawerTitle>{{ role?.name }} permissions</DrawerTitle>
                </div>
                <DrawerDescription>
                    Manage permissions for the selected role.
                </DrawerDescription>
            </DrawerHeader>
            
            <div class="flex flex-wrap gap-4 p-3">
                <Card 
                    v-for="(permissions, group) in permissionGroups" 
                    :key="group" 
                    class="flex-[1_1_300px] min-w-75 max-w-full border-none"
                >
                    <CardHeader>
                        <CardTitle>{{ group }}</CardTitle>
                    </CardHeader>
                    <CardContent>
                       <div
                            v-for="permission in permissions"
                            :key="permission.id"
                            class="flex items-center justify-between gap-2 p-2 rounded-md cursor-pointer hover:bg-muted"
                            @click="togglePermission.mutateAsync({ group, permissionId: permission.id })"
                        >
                        <Label for="permissions-toggle">{{ permission.name }}</Label>

                        <Checkbox
                            id="permissions-toggle"
                            :model-value="permission?.enabled"
                            @click.stop 
                            @update:model-value="togglePermission.mutateAsync({ group, permissionId: permission.id })"
                        />
                        </div>
                    </CardContent>
                </Card>
            </div>
        </DrawerContent>
    </Drawer>
</template>
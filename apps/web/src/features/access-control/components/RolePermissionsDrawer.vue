<script setup lang="ts">
import {
  Drawer,
  DrawerContent,
  DrawerDescription,
  DrawerHeader,
  DrawerTitle,
} from '@/components/ui/drawer'

import { ChevronsLeftIcon } from 'lucide-vue-next';

interface RolePermissionsDrawerProps {
  open: boolean
}

interface Emits {
    (e: 'update:open', open: boolean): void;
}

const { open } = defineProps<RolePermissionsDrawerProps>();
const emit = defineEmits<Emits>();

const handleClose = () => {
    emit('update:open', false);
};
</script>


<template>
    <Drawer :open="open" @update:open="emit('update:open', $event)" direction="right">
        <DrawerContent class="min-w-[70vw]">
            <DrawerHeader>
                <div class="flex align-center gap-3">
                    <ChevronsLeftIcon class="size-5" @click="handleClose" /> 
                    <DrawerTitle>Permissions</DrawerTitle>
                </div>
                <DrawerDescription>
                    Manage permissions for the selected role.
                </DrawerDescription>
            </DrawerHeader>
        </DrawerContent>
    </Drawer>
</template>
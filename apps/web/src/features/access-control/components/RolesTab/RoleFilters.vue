<script setup lang="ts">
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import Card from '@/components/ui/card/Card.vue';
import CardContent from '@/components/ui/card/CardContent.vue';
import Label from '@/components/ui/label/Label.vue';
import type { AcceptableValue } from 'reka-ui';
import type { Role } from '../../types/roles';
import type { useRolesTable } from '../../composables/useRolesTable';
import Input from '@/components/ui/input/Input.vue';
import { ref } from 'vue';

interface RoleFiltersProps {
    roles: Role[] | undefined,
    table: ReturnType<typeof useRolesTable>['table'],
}

const { roles, table } = defineProps<RoleFiltersProps>();

const setRoleFilter = (value: AcceptableValue | null) => {
    table.getColumn('name')?.setFilterValue(value);
}

const setGlobalFilter = (value: AcceptableValue | null) => {
    table.setGlobalFilter(value);
}

const timeoutRef = ref<number | null>(null);
const setDelayedValue = (value: string) => {
    if (timeoutRef.value) {
        clearTimeout(timeoutRef.value);
    }
    timeoutRef.value = window.setTimeout(() => {
       setGlobalFilter(value);
    }, 200);
}

</script>

<template>
    <Card class="border-none shadow-none bg-transparent">
        <CardContent class="p-0">
            <div class="flex items-center gap-3">
                <!------------------------------------------->
                <!-- Role filter by name column (dropdown) -->
                <!------------------------------------------->
                <Label 
                    for="role-filter" 
                    class="text-sm text-muted-foreground whitespace-nowrap"
                >
                    Role
                </Label>

                <!-- Select -->
                <Select @update:modelValue="setRoleFilter">
                    <SelectTrigger
                        id="role-filter"
                        class="w-56 h-9 rounded-lg border border-input bg-background 
                                px-3 text-sm shadow-sm
                                hover:bg-accent hover:text-accent-foreground
                                focus:ring-2 focus:ring-ring focus:ring-offset-1
                                transition-all"
                        >
                        <SelectValue placeholder="All roles" />
                    </SelectTrigger>

                    <SelectContent class="rounded-lg border shadow-md">
                        <!-- All option first (UX improvement) -->
                        <SelectItem :value="null" class="text-muted-foreground">
                            All roles
                        </SelectItem>

                        <div class="my-1 h-px bg-border" />

                        <SelectItem
                            v-for="role in roles"
                            :key="role.id"
                            :value="role.name"
                            class="cursor-pointer"
                        >
                            {{ role.name }}
                        </SelectItem>
                    </SelectContent>
                </Select>

                <!--------------------------------------------------->
                <!-- Free text filter which filters in all columns -->
                <!--------------------------------------------------->
                <Input
                    @keyup="setDelayedValue($event.currentTarget.value)"
                    @keyup.stop.enter="setGlobalFilter($event.currentTarget.value)"
                    placeholder="Search..."
                    class="max-w-75 h-9 rounded-lg border border-input bg-background 
                            px-3 text-sm shadow-sm
                            hover:bg-accent hover:text-accent-foreground
                            focus:ring-2 focus:ring-ring focus:ring-offset-1
                            transition-all"
                />
            </div>
        </CardContent>
    </Card>
</template>
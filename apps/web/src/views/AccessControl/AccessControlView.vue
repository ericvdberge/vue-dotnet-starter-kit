<template>
  <DashboardLayout>
    <div class="space-y-6">
      <!-- Header -->
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-bold">Access Control</h1>
          <p class="text-sm text-muted-foreground">Manage roles and permissions</p>
        </div>
      </div>

      <Tabs v-if="tabs" :defaultValue="tabs[0]?.name">
        <TabsList variant="line">
          <TabsTrigger v-for="tab in tabs" :key="tab.name" :value="tab.name">
            {{ tab.name }}
          </TabsTrigger>
        </TabsList>
        <TabsContent v-for="tab in tabs" :key="tab.name" :value="tab.name">
          <component :is="tab.component" />
        </TabsContent>
      </Tabs>
    </div>
  </DashboardLayout>
</template>

<script setup lang="ts">
import { type Component } from 'vue';
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import DashboardLayout from '@/layouts/DashboardLayout.vue';
import RolesTab from './RolesTab/RolesTab.vue';
import PermissionsTab from './PermissionsTab.vue';
import UsersTab from './UsersTab.vue';

interface Tab {
  name: string;
  component: Component; // Replace with actual component type
}

const tabs: Tab[] = [
  { name: 'Roles', component: RolesTab},
  { name: 'Permissions', component: PermissionsTab },
  { name: 'Users', component: UsersTab }
]
</script>
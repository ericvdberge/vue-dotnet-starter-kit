<template>
  <DashboardLayout>
    <div class="space-y-6">
      <!-- Header -->
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-bold">Access Control</h1>
          <p class="text-sm text-muted-foreground">Manage roles and permissions</p>
        </div>
        <div class="flex gap-2">
          <button class="border px-3 py-1.5 rounded-md text-sm">Apply Template</button>
          <button class="bg-black text-white px-3 py-1.5 rounded-md text-sm">Create Role</button>
        </div>
      </div>

      <!-- Tabs -->
      <div>
        <div class="flex gap-6 border-b">
          <button
            v-for="tab in tabs"
            :key="tab.name"
            @click="setActiveTab(tab.name)"
            :class="[
              'pb-2 text-sm',
              activeTab === tab.name
                ? 'border-b-2 border-black font-medium'
                : 'text-muted-foreground'
            ]"
          >
            {{ tab.name }}
          </button>
        </div>

        <component :is="tabComponents[activeTab]" />
        
      </div>
    </div>
  </DashboardLayout>
</template>

<script setup lang="ts">
import { ref, computed, type Component } from 'vue';
import DashboardLayout from '@/layouts/DashboardLayout.vue';
import RolesTab from './RolesTab.vue';
import PermissionsTab from './PermissionsTab.vue';
import UsersTab from './UsersTab.vue';

interface Tab {
  name: string;
  component: Component; // Replace with actual component type
}
// const tabs = ['Roles', 'Permissions', 'Users', 'Templates'];
const tabs: Tab[] = [
  { name: 'Roles', component: RolesTab},
  { name: 'Permissions', component: PermissionsTab },
  { name: 'Users', component: UsersTab }  
]
const activeTab = ref<string>(tabs[0]!.name);

const setActiveTab = (tabName: string) => {
  const tab = tabs.find(t => t.name === tabName);
  if (tab) activeTab.value = tab.name;
};

const tabComponents = computed(() => {
  const mapping: Record<string, Component> = {};
  tabs.forEach(tab => {
    mapping[tab.name] = tab.component;
  });
  return mapping;
});
</script>
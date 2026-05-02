<script setup lang="ts">
import Tabs from '@/components/ui/tabs/Tabs.vue';
import TabsContent from '@/components/ui/tabs/TabsContent.vue';
import TabsList from '@/components/ui/tabs/TabsList.vue';
import TabsTrigger from '@/components/ui/tabs/TabsTrigger.vue';
import { useAccessControlTabs } from '../composables/useAccessControlTabs';

const { tabs, activeTab } = useAccessControlTabs();
</script>

<template>
    <Tabs v-model="activeTab" class="w-full">
        <TabsList>
            <TabsTrigger v-for="tab in tabs" :key="tab.value" :value="tab.value">
                {{ tab.label }}
            </TabsTrigger>
        </TabsList>
        <TabsContent v-for="tab in tabs" :key="tab.value" :value="tab.value">
            <component :is="tab.component" />
        </TabsContent>
    </Tabs>
</template>
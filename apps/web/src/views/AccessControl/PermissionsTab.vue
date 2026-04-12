<template>

<div class="mt-6">
    <div class="border rounded-xl p-4 bg-white">
        <input
            placeholder="Search permissions..."
            class="border px-2 py-1.5 w-full rounded-md text-sm mb-4"
            v-model="searchText"
        />

        <table class="w-full text-sm">
        <thead class="bg-gray-50">
            <tr>
                <th class="text-left p-3">Key</th>
                <th>Description</th>
                <th>Group</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="p in filteredPermissions" :key="p.key" class="border-t">
                <td class="p-3">{{ p.key }}</td>
                <td>{{ p.desc }}</td>
                <td>{{ p.group }}</td>
            </tr>
        </tbody>
        </table>
    </div>
</div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';

const searchText = ref('');

const permissions = ref([
    { key: 'page.create', desc: 'Create pages', group: 'Pages' },
    { key: 'page.edit', desc: 'Edit pages', group: 'Pages' },
    { key: 'media.upload', desc: 'Upload media', group: 'Media' },
    { key: 'template.apply', desc: 'Apply templates', group: 'Templates' }
]);

const filteredPermissions = computed(() => {
    if (!searchText.value) return permissions.value;
    return permissions.value.filter(p =>
        p.key.includes(searchText.value) ||
        p.desc.includes(searchText.value) ||
        p.group.includes(searchText.value)
    );
});
</script>
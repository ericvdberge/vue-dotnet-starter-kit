<template>
<div class="mt-6">
    <div class="border rounded-xl p-4 bg-white">
        <table class="w-full text-sm">
            <thead class="bg-gray-50">
            <tr>
                <th class="text-left p-3">User</th>
                <th>Role</th>
                <th>Scope</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="u in userTable" :key="u.name" class="border-t">
                <td class="p-3">{{ u.name }}</td>
                <td>{{ u.role }}</td>
                <td>{{ u.scope }}</td>
            </tr>
            </tbody>
        </table>
    </div>
</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const userTable = ref([
  { name: 'John Doe', role: 'Admin', scope: 'Global' },
  { name: 'Jane Smith', role: 'Editor', scope: 'Project A' }
]);
</script>
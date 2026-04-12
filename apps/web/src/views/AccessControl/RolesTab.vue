<template>
<div class="grid grid-cols-12 gap-6 mt-6">
    <!-- Role List -->
    <div class="col-span-3 border rounded-xl p-4 space-y-3 bg-white">
    <input
        v-model="search"
        placeholder="Search roles..."
        class="border px-2 py-1.5 w-full rounded-md text-sm"
    />

    <div
        v-for="role in filteredRoles"
        :key="role"
        @click="selectedRole = role"
        :class="[
        'p-2 rounded-lg cursor-pointer text-sm',
        selectedRole === role ? 'bg-gray-100 font-medium' : ''
        ]"
    >
        {{ role }}
    </div>
    </div>

    <!-- Role Details -->
    <div class="col-span-9 border rounded-xl p-6 bg-white space-y-6">
    <div class="flex justify-between items-start">
        <div class="space-y-1">
        <input
            v-model="selectedRole"
            class="text-lg font-semibold bg-transparent outline-none"
        />
        <p class="text-sm text-gray-500">
            Edit permissions for this role
        </p>
        </div>

        <div class="flex gap-2">
        <button class="border px-3 py-1.5 rounded-md text-sm">Duplicate</button>
        <button class="bg-red-500 text-white px-3 py-1.5 rounded-md text-sm">Delete</button>
        <button class="bg-black text-white px-3 py-1.5 rounded-md text-sm">Save</button>
        </div>
    </div>

    <!-- Permissions Matrix -->
    <div>
        <h3 class="text-sm font-medium mb-3">Permissions</h3>
        <div class="border rounded-lg overflow-hidden">
        <table class="w-full text-sm">
            <thead class="bg-gray-50">
            <tr>
                <th class="text-left p-3">Resource</th>
                <th>Create</th>
                <th>Read</th>
                <th>Update</th>
                <th>Delete</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="res in resources" :key="res" class="border-t">
                <td class="p-3">{{ res }}</td>
                <td v-for="i in 4" :key="i" class="text-center">
                <input type="checkbox" />
                </td>
            </tr>
            </tbody>
        </table>
        </div>
    </div>

    <!-- Users -->
    <div>
        <h3 class="text-sm font-medium mb-2">Assigned Users</h3>
        <div class="flex gap-2 flex-wrap">
        <span
            v-for="user in users"
            :key="user"
            class="bg-gray-100 px-2 py-1 rounded-full text-sm"
        >
            {{ user }}
        </span>
        <button class="border px-3 py-1.5 rounded-md text-sm">+ Assign</button>
        </div>

        <Button @click="showToast">
          Show Toast
        </button>
    </div>
    </div>
</div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';
import { toast } from 'vue-sonner'
import Button from '@/components/ui/button/Button.vue';

const showToast = () => {
  toast.success('This is a toast notification!', { position: 'bottom-right' });
};

const roles = ref(['Admin', 'Editor', 'Viewer']);
const selectedRole = ref('Editor');

const filteredRoles = computed(() =>
  roles.value.filter(r =>
    r.toLowerCase().includes(search.value.toLowerCase())
  )
);

const search = ref('');

const resources = ref(['Pages', 'Media', 'Templates']);
const users = ref(['John Doe', 'Jane Smith']);
</script>
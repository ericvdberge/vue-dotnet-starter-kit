import { defineAsyncComponent, ref } from "vue";
import type { AccessControlTab } from "../types/access-control-tabs";

export const useAccessControlTabs = () => {
    
    const tabs: AccessControlTab[] = [
        { value: 'roles', label: 'Roles', component: defineAsyncComponent(() => import('../components/RolesTab/RolesTab.vue')) },
        { value: 'permissions', label: 'Permissions', component: defineAsyncComponent(() => import('../components/PermissionsTab/PermissionsTab.vue')) },
        { value: 'users', label: 'Users', component: defineAsyncComponent(() => import('../components/UsersTab/UsersTab.vue')) },
    ];

    const activeTab = ref(tabs[0]!.value);


    return { 
        tabs,
        activeTab
    };
}
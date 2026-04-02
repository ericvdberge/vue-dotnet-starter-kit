<template>
    <Sidebar>
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg">
                  <div class="grid flex-1 text-left text-sm leading-tight">
                <span class="truncate font-semibold">Platform X</span>
                <span class="truncate text-xs">Enterprise</span>
              </div>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupLabel>Platform</SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu>
              <SidebarMenuItem v-for="navigationItem in navigationItems" :key="navigationItem.title">
                <SidebarMenuButton as-child>
                  <RouterLink :to="navigationItem.to">
                    <component :is="navigationItem.icon" class="size-4" />
                    <span>{{ navigationItem.title }}</span>
                  </RouterLink>
                </SidebarMenuButton>
              </SidebarMenuItem>
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
      <SidebarFooter>
        <SidebarMenuButton size="lg" @click="logOut">
          <LogOut class="size-4" />
          Logout        
        </SidebarMenuButton>
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
</template>

<script setup lang="ts">
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarRail,
} from '@/components/ui/sidebar'
import { useNavigationItems } from '@/composables/useNavigationItems';
import { RouterLink } from 'vue-router';
import { LogOut } from 'lucide-vue-next';
import keycloak from '@/auth/keycloak';

const navigationItems = useNavigationItems();

const logOut = () => {
  keycloak.logout();
}
</script>
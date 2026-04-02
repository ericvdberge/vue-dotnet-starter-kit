import { createRouter, createWebHistory } from 'vue-router';
import DashboardView from '@/views/DashboardView.vue';
import ProjectView from '@/views/ProjectView.vue';
    
const routes = [
    { path: '/dashboard', name: 'Home', component: DashboardView },
    { path: '/dashboard/projects', name: 'Projects', component: ProjectView },
    { path: '/:pathMatch(.*)*', redirect: '/dashboard' },
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})
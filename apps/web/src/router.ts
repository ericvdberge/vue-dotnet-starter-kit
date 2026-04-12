import { createRouter, createWebHistory } from 'vue-router';
    
const routes = [
    { path: '/dashboard', name: 'Home', component: () => import('@/views/DashboardView.vue') },
    { path: '/dashboard/projects', name: 'Projects', component: () => import('@/views/ProjectView.vue') },
    { path: '/dashboard/access-control', name: 'AccessControl', component: () => import('@/views/AccessControl/AccessControlView.vue') },
    { path: '/:pathMatch(.*)*', redirect: '/dashboard' },
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})
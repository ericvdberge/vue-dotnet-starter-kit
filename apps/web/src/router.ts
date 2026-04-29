import { createRouter, createWebHistory } from 'vue-router';
    
const routes = [
    { path: '/dashboard', name: 'Home', component: () => import('@/features/dashboard/DashboardView.vue') },
    { path: '/dashboard/access-control', name: 'Access Control', component: () => import('@/features/access-control/AccessControlView.vue') },
    { path: '/:pathMatch(.*)*', redirect: '/dashboard' },
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})
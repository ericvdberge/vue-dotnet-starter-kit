import { createRouter, createWebHistory } from 'vue-router';
import DashboardView from '@/views/DashboardView.vue';
import ProjectView from '@/views/ProjectView.vue';
import LoginView from '@/views/LoginView.vue';
    
const routes = [
    { path: '/', name: 'Home', component: DashboardView },
    { path: '/projects', name: 'Projects', component: ProjectView },
    { path: '/login', name: 'Login', component: LoginView },
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})
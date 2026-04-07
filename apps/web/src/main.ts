import { createApp } from 'vue'
import App from './App.vue'
import { router } from './router'
import '@/assets/style.css'
import { useAuth } from './composables/useAuth'
import keycloak from '@/auth/keycloak'

import { VueQueryPlugin, QueryClient } from '@tanstack/vue-query';

router.beforeEach(async (to) => {
  const { isAuthenticated, init } = useAuth()

  await init()

  const isDashboardRoute = to.path.startsWith('/dashboard')

  if (isDashboardRoute && !isAuthenticated.value) {
    await keycloak.login({
      redirectUri: window.location.origin + to.fullPath,
    })
  }

  return true
})

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime:  1000 * 60 * 10, // 10 minutes
    },
  },
})

createApp(App)
  .use(router)
  .use(VueQueryPlugin, { queryClient })
  .mount('#app')

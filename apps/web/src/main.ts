import { createApp } from 'vue'
import App from './App.vue'
import { router } from './router'
import '@/assets/style.css'
import { useAuth } from './composables/useAuth'

import { VueQueryPlugin, QueryClient } from '@tanstack/vue-query';

router.beforeEach(async (to) => {
  const { isAuthenticated, init, login } = useAuth()

  await init()

  const isDashboardRoute = to.path.startsWith('/dashboard')

  if (isDashboardRoute && !isAuthenticated.value) {
    await login()
  }

  return true
})

router.afterEach(() => {
 
  // remove keycloak state from url if exists
  // after login
  if(router.currentRoute.value.hash.includes('#state')) {
    router.replace({
      path: router.currentRoute.value.path,
      query: {},
      hash: '',
    })
  }

  return true;
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

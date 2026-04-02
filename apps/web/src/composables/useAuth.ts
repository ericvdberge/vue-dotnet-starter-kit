// composables/useAuth.ts
import { ref } from 'vue'
import keycloak from '@/auth/keycloak'

const isAuthenticated = ref(false)
const initialized = ref(false)

export const useAuth = () => {
  const init = async () => {
    if (initialized.value) return

    const authenticated = await keycloak.init({
      onLoad: 'check-sso',
      pkceMethod: 'S256',
    })

    isAuthenticated.value = authenticated
    initialized.value = true
  }

  const login = () => {
    return keycloak.login({
      redirectUri: window.location.origin + '/auth/callback',
    })
  }

  const logout = () => {
    return keycloak.logout({
      redirectUri: window.location.origin + '/login',
    })
  }

  const getToken = () => keycloak.token

  return {
    isAuthenticated,
    initialized,
    init,
    login,
    logout,
    getToken,
  }
}
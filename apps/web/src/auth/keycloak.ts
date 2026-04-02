// auth/keycloak.ts
import Keycloak from 'keycloak-js'

const keycloak = new Keycloak({
  url: 'http://localhost:8080',
  realm: 'webgen',
  clientId: 'webgen-spa',
})

export default keycloak
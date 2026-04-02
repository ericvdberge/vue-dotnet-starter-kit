# 🚀 Vue + shadcn + .NET 10 + Keycloak Starter Kit

A modern full‑stack monorepo template combining:

-   **Vue 3 frontend**
-   **shadcn/ui components**
-   **ASP.NET Core (.NET 10) backend**
-   **Keycloak authentication**
-   **Docker Compose for instant setup**

This starter kit lets you start coding right away --- authentication is
**preconfigured and automatic**: visit the app after
`docker-compose up`, and you'll be redirected to Keycloak to login
without any additional setup.

------------------------------------------------------------------------

## ✨ Key Features

-   ⚡ Vue 3 frontend with Vite
-   🎨 shadcn/ui component library
-   🧠 ASP.NET Core (.NET 10) backend
-   🔐 Keycloak authentication (OIDC)
-   🐳 Docker Compose setup
-   📦 Clean monorepo structure
-   📍 Immediate guarded login flow on first visit

------------------------------------------------------------------------

## 🏗️ Project Structure

    root/
    │
    ├── apps/
    │   ├── web/        # Vue.js frontend
    │   └── api/        # .NET 10 Web API
    │
    └── deploy/
        └── docker-compose.yml

------------------------------------------------------------------------

## 🚀 Getting Started

### 🔥 Option 1: Run Everything with Docker (Recommended)

``` bash
cd deploy
docker-compose up --build
```

This brings up:

-   🌐 Frontend (Vue)
-   🧠 Backend API (.NET)
-   🔐 Keycloak

------------------------------------------------------------------------

### 🌍 Services Overview

| Service  | URL                     |
|----------|--------------------------|
| Frontend | http://localhost:3000    |
| API      | https://localhost:5001   |
| Keycloak | http://localhost:8080    |

------------------------------------------------------------------------

## 🔐 Built‑In Authentication (Automatic)

After `docker-compose up`, just open:

👉 **http://localhost:3000**

The app will **immediately redirect** you to Keycloak's login page.\
Use the default credentials:

    Username: testuser  
    Password: test123

This automatic guard is part of the template --- no manual auth setup is
needed. The login flow is preconfigured so you can start building
features instead of wiring auth.

------------------------------------------------------------------------

## 🛠️ Local Development (Without Docker)

### Backend

``` bash
cd apps/api
dotnet restore
dotnet run
```

------------------------------------------------------------------------

### Frontend

``` bash
cd apps/web
npm install
npm run dev
```

------------------------------------------------------------------------

## 🔐 Authentication Flow (Out of the Box)

1.  You visit the frontend (`/`)
2.  The app detects you're not authenticated
3.  It redirects to Keycloak
4.  You login (e.g., testuser/test123)
5.  Keycloak redirects back
6.  You're authenticated and can use protected routes

------------------------------------------------------------------------

## ⚙️ Configuration

### Frontend (`apps/web/.env`)

    VITE_API_BASE_URL=https://localhost:5001
    VITE_KEYCLOAK_URL=http://localhost:8080
    VITE_KEYCLOAK_REALM=your‑realm
    VITE_KEYCLOAK_CLIENT_ID=your‑client‑id

------------------------------------------------------------------------

### Backend (`apps/api/appsettings.json`)

``` json
{
  "Keycloak": {
    "Authority": "http://keycloak:8080/realms/your‑realm",
    "Audience": "your‑client‑id"
  }
}
```

------------------------------------------------------------------------

## 🐳 Docker Notes

-   Services run on a shared Docker network
-   Hostnames inside containers match service names (`keycloak`, `api`)
-   Environment variables can be overridden in `docker-compose.yml`

------------------------------------------------------------------------

## 🤝 Contributing

1.  Fork the repo
2.  Create a feature branch
3.  Open a pull request

------------------------------------------------------------------------

## 📄 License

MIT License

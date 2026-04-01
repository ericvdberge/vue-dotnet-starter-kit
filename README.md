# 🚀 Vue + shadcn + .NET 10 + Keycloak Template

A full-stack monorepo template combining a modern Vue frontend, a .NET 10 Web API, and Keycloak authentication — fully containerized with Docker Compose.

Designed for fast local development and easy production deployment.

---

## ✨ Features

* ⚡ **Vue.js frontend**
* 🎨 **shadcn/ui components**
* 🧠 **.NET 10 Web API**
* 🔐 **Keycloak authentication (OIDC)**
* 🐳 **Docker Compose setup**
* 📦 **Clean monorepo structure**

---

## 🏗️ Project Structure

```
root/
│
├── apps/
│   ├── web/        # Vue.js + shadcn frontend
│   └── api/        # .NET 10 Web API
│
└── deploy/
    └── docker-compose.yml
```

---

## 🚀 Getting Started

### 🔥 Option 1: Run Everything with Docker (Recommended)

```
cd deploy
docker-compose up --build
```

This will start:

* 🌐 Frontend (Vue)
* 🧠 Backend API (.NET)
* 🔐 Keycloak

---

### 🌍 Services Overview

| Service  | URL                    |
| -------- | ---------------------- |
| Frontend | http://localhost:3000  |
| API      | https://localhost:5001 |
| Keycloak | http://localhost:8080  |

---

### 🔐 Keycloak Setup (First Time)

1. Open: http://localhost:8080
2. Login with admin credentials (from `docker-compose.yml`)
3. Create:

   * Realm
   * Client (SPA)
4. Configure:

   * Redirect URI: `http://localhost:3000/*`

---

## 🛠️ Local Development (Without Docker)

### Backend

```
cd apps/api
dotnet restore
dotnet run
```

---

### Frontend

```
cd apps/web
npm install
npm run dev
```

---

## 🔐 Authentication Flow

1. User clicks **Login**
2. Redirect to Keycloak
3. User authenticates
4. Access token is returned
5. Frontend stores token
6. Token is sent with API requests
7. API validates JWT

---

## ⚙️ Configuration

### Frontend (`apps/web/.env`)

```
VITE_API_BASE_URL=https://localhost:5001
VITE_KEYCLOAK_URL=http://localhost:8080
VITE_KEYCLOAK_REALM=your-realm
VITE_KEYCLOAK_CLIENT_ID=your-client-id
```

---

### Backend (`apps/api/appsettings.json`)

```
{
  "Keycloak": {
    "Authority": "http://keycloak:8080/realms/your-realm",
    "Audience": "your-client-id"
  }
}
```

> 💡 Note: When running inside Docker, use `keycloak` as the hostname (service name).

---

## 🐳 Docker Notes

* Services communicate via Docker network
* Service names act as hostnames (e.g. `keycloak`, `api`)
* Environment variables can be overridden in `docker-compose.yml`

---

## 🧪 Roadmap

* [ ] Seed Keycloak realm automatically
* [ ] HTTPS setup in Docker
* [ ] Role-based authorization
* [ ] CI/CD pipeline
* [ ] Production-ready compose / Helm charts

---

## 🤝 Contributing

1. Fork the repo
2. Create a feature branch
3. Open a PR

---

## 📄 License

MIT License

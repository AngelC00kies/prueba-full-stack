# 🛒 Sistema de Gestión de Ventas — Prueba Técnica Full-Stack

Aplicación web completa para la gestión de productos, clientes y ventas, desarrollada como prueba técnica para el rol de **Programador Full-Stack**. Incluye un **backend REST** en .NET 8 con autenticación JWT y un **frontend SPA** en Vue 3 con Composition API.

---

## Tabla de Contenidos

- [Descripción](#-descripción)
- [Tecnologías Utilizadas](#-tecnologías-utilizadas)
- [Arquitectura](#-arquitectura)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación y Configuración](#-instalación-y-configuración)
- [Cómo Ejecutar](#-cómo-ejecutar)
- [Endpoints Principales](#-endpoints-principales)
- [Pruebas Unitarias](#-pruebas-unitarias)
- [Usuario de Prueba](#-usuario-de-prueba)
- [Autor](#-autor)

---

## Descripción

Este proyecto implementa un **sistema de gestión de ventas** que permite:

- **Autenticación segura** con JWT y contraseñas hasheadas con BCrypt.
- **CRUD completo de productos** con control de stock.
- **CRUD completo de clientes** con validación de datos.
- **Registro de ventas** con descuento automático de stock y cálculo de totales.
- **Historial de ventas** con detalles por transacción.
- **Dashboard** con estadísticas en tiempo real.

La aplicación aplica **arquitectura en capas**, **principios SOLID**, **patrón Repository**, **DTOs** y **pruebas unitarias**, siguiendo las mejores prácticas del ecosistema .NET + Vue.

---

## Tecnologías Utilizadas

### Backend

| Tecnología                                           | Versión | Uso                               |
| ---------------------------------------------------- | ------- | --------------------------------- |
| C#                                                   | 12      | Lenguaje principal                |
| .NET                                                 | 8.0     | Framework                         |
| ASP.NET Core Web API                                 | 8.0     | API REST                          |
| Entity Framework Core                                | 8.0     | ORM                               |
| Entity Framework Core.SqlServer                      | 8.0     | Provider para SQL Server          |
| Entity Framework Core.Tools                          | 8.0     | Herramientas de migraciones       |
| Entity Framework Core.Design                         | 8.0     | Soporte en tiempo de diseño       |
| SQL Server                                           | 2019+   | Base de datos relacional          |
| Microsoft.AspNetCore.Authentication.JwtBearer        | 8.0     | Autenticación JWT                 |
| System.IdentityModel.Tokens.Jwt                      | 8.x     | Generación y validación de tokens |
| Microsoft.IdentityModel.Tokens                       | 8.x     | Tokens de seguridad               |
| BCrypt.Net-Next                                      | 4.x     | Hashing de contraseñas            |
| FluentValidation                                     | 11.x    | Validación de DTOs                |
| FluentValidation.DependencyInjectionExtensions       | 11.x    | Registro en DI                    |
| FluentValidation.AspNetCore                          | 11.x    | Integración con ASP.NET Core      |
| Swashbuckle.AspNetCore                               | 6.x     | Documentación Swagger             |
| Microsoft.Extensions.Options.ConfigurationExtensions | 8.x     | Configuración tipada              |
| Microsoft.Extensions.Configuration.Abstractions      | 8.x     | Lectura de configuración          |

### Testing

| Tecnología                             | Versión | Uso                        |
| -------------------------------------- | ------- | -------------------------- |
| xUnit                                  | 2.x     | Framework de pruebas       |
| Moq                                    | 4.x     | Mocking de dependencias    |
| FluentAssertions                       | 6.x     | Assertions legibles        |
| Microsoft.EntityFrameworkCore.InMemory | 8.x     | BD en memoria para pruebas |
| Microsoft.NET.Test.Sdk                 | 17.x    | SDK de pruebas             |
| xunit.runner.visualstudio              | 2.x     | Runner de xUnit en VS      |
| coverlet.collector                     | 6.x     | Cobertura de código        |

### Frontend

| Tecnología      | Versión          | Uso                             |
| --------------- | ---------------- | ------------------------------- |
| Vue.js          | 3.5              | Framework SPA                   |
| Vite            | 5.x              | Bundler y dev server            |
| Vue Router      | 4.x              | Navegación SPA                  |
| Pinia           | 2.x              | Estado global                   |
| Axios           | 1.x              | Cliente HTTP                    |
| Bootstrap Icons | 1.x              | Iconografía profesional         |
| Composition API | `<script setup>` | Sintaxis moderna de componentes |

---

## Arquitectura

El backend sigue **arquitectura en capas** con separación clara de responsabilidades:

```
┌─────────────────────────────────────────┐
│   PruebaTecnica.API (Controllers)       │  ← Capa de presentación
├─────────────────────────────────────────┤
│   PruebaTecnica.Application (Services)  │  ← Lógica de negocio
├─────────────────────────────────────────┤
│   PruebaTecnica.Infrastructure (EF)     │  ← Acceso a datos
├─────────────────────────────────────────┤
│   PruebaTecnica.Domain (Entities)       │  ← Núcleo del dominio
└─────────────────────────────────────────┘
```

### Principios aplicados

- **SOLID** — Inyección de dependencias, interfaces segregadas, responsabilidad única.
- **Patrón Repository** — Abstracción del acceso a datos.
- **DTO Pattern** — Separación entre entidades y contratos de API.
- **Dependency Injection** — Registro centralizado en `DependencyInjection.cs`.
- **Fluent Validation** — Validación desacoplada de los DTOs.
- **Middleware de errores** — Manejo centralizado de excepciones.
- **Código comentado** — XML Documentation Comments en clases y métodos públicos.

---

## Estructura del Proyecto

```
prueba-full-stack/
│
├── backend/
│   ├── database/
│   │   └── script.sql                          # Script SQL de la base de datos
│   │
│   └── PruebaFullStack/
│       ├── PruebaFullStack.sln
│       ├── PruebaTecnica.API/                  # Capa de presentación
│       │   ├── Controllers/
│       │   │   ├── AuthController.cs
│       │   │   ├── ProductosController.cs
│       │   │   ├── ClientesController.cs
│       │   │   └── VentasController.cs
│       │   ├── Middleware/
│       │   │   └── ErrorHandlingMiddleware.cs
│       │   ├── Extensions/
│       │   │   └── SwaggerExtensions.cs
│       │   ├── Properties/
│       │   │   └── launchSettings.json
│       │   ├── Program.cs
│       │   ├── appsettings.json
│       │   └── appsettings.Development.json
│       │
│       ├── PruebaTecnica.Application/          # Lógica de negocio
│       │   ├── Common/
│       │   │   └── Result.cs
│       │   ├── DTOs/
│       │   │   ├── Auth/
│       │   │   ├── Productos/
│       │   │   ├── Clientes/
│       │   │   └── Ventas/
│       │   ├── Interfaces/
│       │   │   ├── Repositories/
│       │   │   ├── IProductoService.cs
│       │   │   ├── IClienteService.cs
│       │   │   ├── IVentaService.cs
│       │   │   ├── IAuthService.cs
│       │   │   ├── ITokenGenerator.cs
│       │   │   └── IPasswordHasher.cs
│       │   ├── Services/
│       │   │   ├── ProductoService.cs
│       │   │   ├── ClienteService.cs
│       │   │   ├── VentaService.cs
│       │   │   └── AuthService.cs
│       │   └── Validators/
│       │       ├── CrearProductoValidator.cs
│       │       ├── CrearClienteValidator.cs
│       │       ├── CrearVentaValidator.cs
│       │       ├── LoginValidator.cs
│       │       └── RegisterValidator.cs
│       │
│       ├── PruebaTecnica.Domain/               # Entidades del dominio
│       │   └── Entities/
│       │       ├── Producto.cs
│       │       ├── Cliente.cs
│       │       ├── Venta.cs
│       │       ├── DetalleVenta.cs
│       │       └── Usuario.cs
│       │
│       ├── PruebaTecnica.Infrastructure/       # Acceso a datos
│       │   ├── Data/
│       │   │   └── ApplicationDbContext.cs
│       │   ├── Migrations/
│       │   ├── Repositories/
│       │   │   ├── ProductoRepository.cs
│       │   │   ├── ClienteRepository.cs
│       │   │   ├── VentaRepository.cs
│       │   │   └── UsuarioRepository.cs
│       │   ├── Security/
│       │   │   ├── JwtTokenGenerator.cs
│       │   │   ├── JwtSettings.cs
│       │   │   └── PasswordHasher.cs
│       │   └── DependencyInjection.cs
│       │
│       └── PruebaTecnica.Tests/                # Pruebas unitarias
│           ├── Builders/
│           │   ├── ProductoBuilder.cs
│           │   ├── ClienteBuilder.cs
│           │   ├── VentaBuilder.cs
│           │   └── UsuarioBuilder.cs
│           ├── Services/
│           │   ├── ProductoServiceTests.cs
│           │   ├── ClienteServiceTests.cs
│           │   ├── VentaServiceTests.cs
│           │   └── AuthServiceTests.cs
│           └── Validators/
│               ├── CrearProductoValidatorTests.cs
│               ├── CrearClienteValidatorTests.cs
│               └── CrearVentaValidatorTests.cs
│
└── frontend/
    ├── public/
    ├── src/
    │   ├── assets/
    │   │   └── styles/
    │   │       └── main.css
    │   ├── components/
    │   │   └── common/
    │   │       ├── Navbar.vue
    │   │       ├── Sidebar.vue
    │   │       └── Loader.vue
    │   ├── layouts/
    │   │   └── DefaultLayout.vue
    │   ├── router/
    │   │   └── index.js
    │   ├── services/
    │   │   ├── api.js
    │   │   ├── authService.js
    │   │   ├── productoService.js
    │   │   ├── clienteService.js
    │   │   └── ventaService.js
    │   ├── store/
    │   │   ├── auth.js
    │   │   ├── productos.js
    │   │   ├── clientes.js
    │   │   └── ventas.js
    │   ├── views/
    │   │   ├── LoginView.vue
    │   │   ├── DashboardView.vue
    │   │   ├── ProductosView.vue
    │   │   ├── ClientesView.vue
    │   │   ├── VentasView.vue
    │   │   └── HistorialVentasView.vue
    │   ├── App.vue
    │   └── main.js
    ├── .env
    ├── index.html
    ├── package.json
    └── vite.config.js
```

---

## Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- **.NET SDK 8.0** o superior → [Descargar](https://dotnet.microsoft.com/download)
- **SQL Server 2019** o superior (Express, Developer, o LocalDB) → [Descargar](https://www.microsoft.com/sql-server/sql-server-downloads)
- **SQL Server Management Studio (SSMS)** (opcional pero recomendado) → [Descargar](https://aka.ms/ssmsfullsetup)
- **Node.js 18+** y **npm 9+** → [Descargar](https://nodejs.org/)
- **Git** → [Descargar](https://git-scm.com/)
- **Visual Studio 2022** o **VS Code** (opcional)
- **dotnet-ef CLI** → `dotnet tool install --global dotnet-ef --version 8.*`

---

## Instalación y Configuración

### 1️ Clonar el repositorio

```bash
git clone https://github.com/AngelC00kies/prueba-full-stack.git
cd prueba-full-stack
```

### 2️ Configurar la base de datos

**Opción A — Ejecutar el script SQL (recomendado):**

1. Abre **SSMS** y conéctate a tu instancia de SQL Server.
2. Abre el archivo `backend/database/script.sql`.
3. Ejecuta el script (F5). Esto creará la base de datos `GestionVentasDB` con todas las tablas.

**Opción B — Aplicar migraciones de EF Core:**

Desde la raíz del repo:

```bash
dotnet ef database update \
  --project backend/PruebaFullStack/PruebaTecnica.Infrastructure \
  --startup-project backend/PruebaFullStack/PruebaTecnica.API
```

### 3️ Configurar la cadena de conexión

Abre `backend/PruebaFullStack/PruebaTecnica.API/appsettings.json` y ajusta la cadena de conexión a tu instancia de SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GestionVentasDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "EstaEsUnaClaveSuperSecretaParaJWTDeAlMenos32Caracteres!",
    "Issuer": "PruebaTecnicaAPI",
    "Audience": "PruebaTecnicaClient",
    "ExpirationHours": 2
  }
}
```

> 4 **Ejemplos de `Server=` según tu instancia:**
>
> - `Server=localhost\SQLEXPRESS`
> - `Server=DESKTOP-XXXXX`
> - `Server=(localdb)\MSSQLLocalDB`

### Instalar dependencias del frontend

```bash
cd frontend
npm install --legacy-peer-deps
```

### 5️ Configurar la URL del API en el frontend

Abre `frontend/.env` y verifica que apunte al puerto correcto de tu API (por defecto `7111`):

```env
VITE_API_URL=https://localhost:7111/api
```

> El puerto lo puedes ver en `backend/PruebaFullStack/PruebaTecnica.API/Properties/launchSettings.json`.

---

## Cómo Ejecutar

Se necesitan **dos terminales en paralelo**: una para el backend y otra para el frontend.

### Terminal 1 — Backend

```bash
cd backend/PruebaFullStack/PruebaTecnica.API
dotnet run
```

Salida esperada:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7111
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5111
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

**Verificar que funciona:** abre `https://localhost:7111/swagger` en el navegador. Deberías ver la documentación interactiva de la API.

### Terminal 2 — Frontend

```bash
cd frontend
npm run dev
```

Salida esperada:

```
  VITE v5.x.x  ready in 500 ms

  ➜  Local:   http://localhost:5173/
  ➜  Network: use --host to expose
```

**Verificar que funciona:** abre `http://localhost:5173` en el navegador. Deberías ver la pantalla de login.

---

## Endpoints Principales

### Auth

| Método | Endpoint             | Descripción          | Auth |
| ------ | -------------------- | -------------------- | ---- |
| `POST` | `/api/auth/register` | Registrar usuario    | ❌   |
| `POST` | `/api/auth/login`    | Iniciar sesión → JWT | ❌   |

### Productos

| Método   | Endpoint              | Descripción    | Auth |
| -------- | --------------------- | -------------- | ---- |
| `GET`    | `/api/productos`      | Listar todos   | ✅   |
| `GET`    | `/api/productos/{id}` | Obtener por id | ✅   |
| `POST`   | `/api/productos`      | Crear          | ✅   |
| `PUT`    | `/api/productos/{id}` | Actualizar     | ✅   |
| `DELETE` | `/api/productos/{id}` | Eliminar       | ✅   |

### Clientes

| Método   | Endpoint             | Descripción    | Auth |
| -------- | -------------------- | -------------- | ---- |
| `GET`    | `/api/clientes`      | Listar todos   | ✅   |
| `GET`    | `/api/clientes/{id}` | Obtener por id | ✅   |
| `POST`   | `/api/clientes`      | Crear          | ✅   |
| `PUT`    | `/api/clientes/{id}` | Actualizar     | ✅   |
| `DELETE` | `/api/clientes/{id}` | Eliminar       | ✅   |

### Ventas

| Método | Endpoint           | Descripción     | Auth |
| ------ | ------------------ | --------------- | ---- |
| `GET`  | `/api/ventas`      | Listar todas    | ✅   |
| `GET`  | `/api/ventas/{id}` | Obtener por id  | ✅   |
| `POST` | `/api/ventas`      | Registrar venta | ✅   |

**Ejemplo de payload para crear una venta:**

```json
{
  "idCliente": 1,
  "detalles": [{ "idProducto": 1, "cantidad": 2 }]
}
```

---

## 🧪 Pruebas Unitarias

El proyecto incluye **51 pruebas unitarias** que cubren los servicios y validadores de la capa Application.

### Ejecutar todas las pruebas

```bash
cd backend/PruebaFullStack
dotnet test PruebaTecnica.Tests
```

Salida esperada:

```
Correctas! - Con error: 0, Superado: 51, Omitido: 0, Total: 51
```

### Cobertura de pruebas

| Archivo                          | Tests  | Cubre                                  |
| -------------------------------- | ------ | -------------------------------------- |
| `ProductoServiceTests.cs`        | 8      | CRUD completo + casos de error         |
| `ClienteServiceTests.cs`         | 8      | CRUD completo + casos de error         |
| `VentaServiceTests.cs`           | 6      | Validación stock + cálculo + descuento |
| `AuthServiceTests.cs`            | 7      | Login, registro, hashing               |
| `CrearProductoValidatorTests.cs` | 7      | Reglas de validación                   |
| `CrearClienteValidatorTests.cs`  | 7      | Reglas de validación                   |
| `CrearVentaValidatorTests.cs`    | 4      | Reglas de validación                   |
| **Total**                        | **51** |                                        |

---

## Usuario de Prueba

Puedes registrar un usuario nuevo desde la pantalla de registro, o usar el siguiente si ya lo creaste:

| Campo          | Valor      |
| -------------- | ---------- |
| **Usuario**    | `admin`    |
| **Contraseña** | `admin123` |
| **Rol**        | `admin`    |

**O registrar uno nuevo desde Swagger/curl:**

```bash
curl -k -X POST https://localhost:7111/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123","rol":"admin"}'
```

---

## Características Destacadas

### Backend

- **Arquitectura en capas** con separación Domain/Application/Infrastructure/API.
- **Autenticación JWT** con expiración configurable (2 horas por defecto).
- **Hashing BCrypt** para contraseñas.
- **Validación con FluentValidation** en todos los DTOs.
- **Middleware global de errores** con respuestas JSON consistentes.
- **Swagger con soporte para JWT Bearer** (botón Authorize).
- **CORS configurado** para el frontend.
- **Migraciones EF Core** para la BD.
- **Código comentado** con XML Documentation Comments.
- **51 pruebas unitarias** con xUnit + Moq + FluentAssertions.

### Frontend

- **Vue 3 + Composition API** con `<script setup>`.
- **Pinia** para estado global.
- **Vue Router** con guards de autenticación.
- **Axios con interceptores** para adjuntar el token automáticamente.
- **Diseño responsive** (móvil, tablet, escritorio).
- **Bootstrap Icons** para iconografía profesional.
- **Modales dinámicos** para creación/edición.
- **Manejo de errores** con alertas visuales.
- **Logout automático** al expirar el token.
- **Buscador en tiempo real** en las vistas de productos, clientes e historial.

---

## Flujo de Trabajo con Git

El proyecto se desarrolló siguiendo **Git Flow** con ramas por feature:

```
main
 └── develop
      ├── chore/setup-backend
      ├── feature/domain-entities
      ├── feature/application-services
      ├── feature/infrastructure-ef
      ├── feature/api-controllers
      ├── test/unit-tests
      └── feature/frontend-setup
```

### Convención de commits

Se usó **Conventional Commits**:

- `feat:` nueva funcionalidad
- `fix:` corrección de bug
- `chore:` tareas de configuración
- `test:` pruebas unitarias
- `docs:` documentación

---

## Autor

**Angel C00kies**

- GitHub: [@AngelC00kies](https://github.com/AngelC00kies)
- Proyecto: [prueba-full-stack](https://github.com/AngelC00kies/prueba-full-stack)

---

# 📘 Sistema de Gestión de Pagos – Cliente MVC (.NET 8)

## 📝 Descripción General

El Cliente MVC es una aplicación web desarrollada en **ASP.NET MVC (.NET 8)** que consume la **Web API del Sistema de Gestión de Pagos** mediante **HttpClient**.
Permite a los usuarios autenticarse, registrar pagos, consultar reportes y administrar la información del sistema según su rol: Administrador, Gerente o Empleado.

👉 La Web API utilizada por este cliente se encuentra en un repositorio separado:
`https://github.com/codewitheduardo/API-Obl-P3-ago25` *(reemplazar con el real)*

✔ Comunicación con la API mediante JWT
✔ Manejo de sesión
✔ Uso de servicios HttpClient para todas las operaciones
✔ Interfaz responsive con Bootstrap

---

# 🌐 Aplicación desplegada en Azure

🎯 **Cliente MVC:**
👉 [https://obligatoriop3imem-ecasbyafbsfbdwhw.canadacentral-01.azurewebsites.net](https://obligatoriop3imem-ecasbyafbsfbdwhw.canadacentral-01.azurewebsites.net)

La aplicación se encuentra funcionando 100% en la nube.

---

# 📂 Arquitectura del Cliente MVC

```
/Controllers   → Controladores que consumen la Web API
/Models        → DTOs y modelos para las vistas
/Views         → Razor Views con Bootstrap
/Services      → Servicios HttpClient (Login, Pagos, Usuarios, TiposGasto…)
/wwwroot       → Archivos estáticos (CSS, JS, imágenes)
```

✔ Token JWT almacenado en Session
✔ Requests HTTP firmadas con `Authorization: Bearer <token>`
✔ Manejo de errores y reintentos
✔ Validaciones en servidor y cliente

---

# 🔐 Autenticación con JWT

Flujo implementado:

1. El usuario ingresa credenciales
2. MVC envía `POST /api/Auth/Login`
3. La API devuelve un **JWT**
4. Se almacena en Session
5. Todas las llamadas posteriores incluyen:

   ```
   Authorization: Bearer <token>
   ```
6. Si expira → deslogueo automático

---

# 🧭 Funcionalidades del Cliente MVC

## 🔒 Login y Logout

Autenticación completa utilizando la Web API.

---

## 💸 Alta de Pagos (Empleado / Gerente / Admin)

Formulario para registrar:

* Pagos únicos
* Pagos recurrentes

Consume:

```
POST /api/Pagos
```

---

## 🧾 Listado Mensual de Pagos (Gerentes)

Permite filtrar por mes y año.
Consume endpoint de la API y muestra:

* Listado de pagos
* Totales calculados
* Saldos pendientes

---

## 👤 Gestión de Usuarios (Admin / Gerente)

* Alta de usuarios
* Roles: Empleado / Gerente
* Validaciones de formularios

---

## 🧩 Tipos de Gasto (Sólo Administradores)

Operaciones completas:

```
POST /api/TiposGasto
PUT /api/TiposGasto/{id}
DELETE /api/TiposGasto/{id}
```

Se controla:

* No eliminar tipos en uso
* Mostrar mensajes claros al usuario

---

## 🔄 Reset de Contraseña (Administradores)

Consume:

```
PUT /api/Usuarios/{id}/ResetPassword
```

La nueva contraseña se muestra en pantalla.

---

## 🧠 Reportes y Consultas (Gerentes)

### Usuarios con pagos superiores a un monto

```
GET /api/Usuarios/PagosSuperioresA/{monto}
```

### Equipos con pagos únicos mayores a un valor

```
GET /api/Equipos/PagosUnicosMayorA/{monto}
```

Presentación ordenada en tablas con Bootstrap.

---

# ⚙️ Configuración del proyecto

### `appsettings.json`

Debe incluir la URL BASE de la API:

```json
{
  "ApiBaseUrl": "https://TU-API.azurewebsites.net/api"
}
```

*(Pegar aquí la URL real de tu API)*

---

# ☁️ Deploy en Azure

## ✔ Cliente MVC desplegado en:

[https://obligatoriop3imem-ecasbyafbsfbdwhw.canadacentral-01.azurewebsites.net](https://obligatoriop3imem-ecasbyafbsfbdwhw.canadacentral-01.azurewebsites.net)

### Configuraciones en Azure App Service:

* Variable `ApiBaseUrl` configurada en *Application Settings*
* HTTPS obligatorio
* Plataforma .NET 8 seleccionada
* Archivos estáticos habilitados

---

# 🔗 Repositorios Relacionados

| Proyecto                           | Repositorio                                                      |
| ---------------------------------- | ---------------------------------------------------------------- |
| **Web API**                        | `https://github.com/codewitheduardo/API-Obl-P3-ago25`            |
| **Cliente MVC (este repositorio)** | `https://github.com/codewitheduardo/MVC-Obl-P3-ago25*`           |

---

# 📦 Dependencias

* ASP.NET MVC (.NET 8)
* HttpClient
* Bootstrap 5
* Session Middleware
* Razor Pages

---

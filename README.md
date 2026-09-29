# Sistema de Gestión Central - Microservicios & Blazor WebAssembly

Este proyecto es una solución integral orientada a dominios (DDD) y basada en Arquitectura Limpia (Clean Architecture). Está compuesto por tres microservicios independientes en el backend y una aplicación web cliente (SPA) en el frontend, diseñados para gestionar Clientes, Productos y Órdenes de compra.

## 🚀 Tecnologías y Herramientas

**Backend (Microservicios):**
* **C# / .NET 8** (Web API)
* **Entity Framework Core** (SQL Server, Code-First)
* **Arquitectura:** Clean Architecture, Domain-Driven Design (DDD)
* **Patrones:** Repository Pattern, Aggregate Roots, Value Objects
* **Validación:** FluentValidation
* **Mapeo:** AutoMapper
* **Logging:** Serilog (Registro estructurado en archivos `.txt`)
* **Manejo de Errores:** Middleware global de excepciones personalizado
* **Seguridad/Red:** Políticas CORS configuradas por microservicio

**Frontend (SPA):**
* **Blazor WebAssembly** (Aplicación web independiente)
* **Diseño:** Bootstrap 5 (UI responsiva, Tarjetas de Dashboard)
* **Comunicación:** `IHttpClientFactory` para consumo simultáneo de múltiples APIs

---

## ⚙️ Arquitectura de la Solución

La solución está separada lógicamente en microservicios, cada uno con su propia base de datos y divididos en 4 capas conceptuales:

```text
📦 Solucion
 ┣ 📂 Customer (Microservicio)
 ┃ ┣ 📜 Customer.API (Controladores, Middleware, Serilog)
 ┃ ┣ 📜 Customer.Application (Servicios, DTOs, FluentValidation)
 ┃ ┣ 📜 Customer.Domain (Entidades, AddressVO, Interfaces)
 ┃ ┗ 📜 Customer.Infrastructure (DbContext, Repositorios)
 ┣ 📂 Product (Microservicio)
 ┃ ┗ 📜 ... (Misma estructura Clean Architecture)
 ┣ 📂 Order (Microservicio)
 ┃ ┗ 📜 ... (Misma estructura Clean Architecture)
 ┗ 📂 Frontend
   ┗ 📜 Frontend.Blazor (UI, Pages, HTTP Services, Modelos)

✨ Características y Módulos del Sistema
1. Panel Principal (Dashboard)
Interfaz de inicio con accesos directos (Cards) a los módulos principales del sistema.

Navegación lateral limpia y responsiva adaptada con Bootstrap.

2. Gestión de Clientes (Customer API)
Value Objects: La dirección del cliente está modelada como un Value Object (AddressVO), separando internamente Calle, Ciudad y País, pero manteniéndolo atado a la entidad raíz mediante .OwnsOne().

Endpoints (CRUD): Creación (POST), Lectura (GET), Actualización (PUT), Eliminación (DELETE).

Interfaz: Tabla de visualización en tiempo real y formulario interactivo de alta.

3. Gestión de Productos (Product API)
Administración de catálogo, precios y control de stock disponible.

Endpoints (CRUD): Creación (POST), Lectura (GET), Actualización (PUT), Eliminación (DELETE).

Interfaz: Tabla de visualización de inventario y formulario de registro.

4. Gestión de Órdenes (Order API)
Generación de nuevas órdenes de compra cruzando datos en tiempo real del microservicio de Clientes y Productos.

Historial detallado de operaciones (Fecha, Total de la Orden, Cliente, Items comprados).

Endpoints: Historial completo (GET), Generación de Orden (POST).

Interfaz: Formulario dinámico con selectores alimentados por los microservicios y tabla de historial con desglose de items.

🛠️ Requisitos Previos
Visual Studio 2022 (o superior) con la carga de trabajo "Desarrollo de ASP.NET y web".

.NET 8 SDK.

SQL Server LocalDB (incluido con Visual Studio) o SQL Server Express.

🚀 Instrucciones de Ejecución (Paso a Paso)
1. Generar las Bases de Datos (Migraciones)
El sistema utiliza el enfoque Code-First. Debes aplicar las migraciones para generar las tablas en tu servidor SQL local.
Abre la Consola del Administrador de Paquetes (Package Manager Console) y ejecuta los siguientes comandos uno por uno:

PowerShell
Update-Database -Project Customer.Infrastructure -StartupProject Customer.API
Update-Database -Project Product.Infrastructure -StartupProject Product.API
Update-Database -Project Order.Infrastructure -StartupProject Order.API

2. Configurar Múltiples Proyectos de Inicio
Para que el frontend Blazor pueda comunicarse con las 3 APIs, todo debe correr simultáneamente:

Haz clic derecho sobre la Solución en el Explorador de soluciones -> Propiedades.

Selecciona Proyecto de inicio -> Múltiples proyectos de inicio.

Cambia la acción a "Iniciar" para los 4 proyectos principales:

Customer.API

Product.API

Order.API

Frontend.Blazor

Haz clic en Aplicar y Aceptar.

3. Ejecutar la Aplicación
Presiona F5 en Visual Studio.

Se abrirán automáticamente tres pestañas de Swagger (documentación de las APIs) y la aplicación web Blazor.

Nota: Todos los errores controlados o de servidor (500) generados en la API de Clientes se guardarán automáticamente en un archivo de texto dentro de la carpeta /Logs gracias a Serilog.

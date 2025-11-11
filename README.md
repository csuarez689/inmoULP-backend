# 🏢 Inmobiliaria API

# 📺 Enlace video muestra depuración funcionalidades Agregar Inmueble / Obtener Inmueble. [Ver](https://drive.google.com/file/d/17aIGbuRmvnbrUETvRtqr_ICQ8A3E7jd6/view?usp=sharing)

API REST desarrollada en ASP.NET Core para gestionar propietarios, inmuebles, contratos y pagos de la inmobiliaria ULP. Expone endpoints protegidos con JWT, persiste datos en MySQL y publica recursos estáticos (imágenes) para el cliente móvil Android.

## 🚀 Tecnologías utilizadas

-   .NET 8 (ASP.NET Core Web API)
-   Entity Framework Core + Pomelo MySql Provider
-   Autenticación JWT
-   Serilog para logging
-   Docker & Docker Compose
-   Swagger / OpenAPI

## 📋 Requisitos previos

| Herramienta    | Versión recomendada |
| -------------- | ------------------- |
| .NET SDK       | 8.0.x               |
| MySQL          | 8.0.x               |
| Docker         | 24+ _(opcional)_    |
| Docker Compose | 2.x _(opcional)_    |

## 🔐 Configuración de variables y secretos

La API lee la configuración desde `appsettings.json`, variables de entorno o _user-secrets_. Se recomienda no versionar credenciales reales.

Un ejemplo mínimo para un perfil local (`appsettings.Development.json` o `dotnet user-secrets`) es el siguiente:

```json
{
	"Salt": "<tu-salt-seguro>",
	"ConnectionStrings": {
		"DefaultConnection": "Server=localhost;Port=3306;Database=<nombre_bd>;User=<usuario>;Password=<password>;"
	},
	"Jwt": {
		"Secret": "<jwt-secret>",
		"Issuer": "<issuer>",
		"Audience": "<audience>",
		"ExpirationHours": 8
	}
}
```

Podés definir los mismos valores mediante variables de entorno (`ConnectionStrings__DefaultConnection`, `Jwt__Secret`, etc.) o `dotnet user-secrets`. Recordá reemplazar los placeholders por tus credenciales y rotar los secretos antes de desplegar en producción.

## ▶️ Ejecución local (sin Docker)

1. Clonar el repositorio y posicionarse en la carpeta `InmobiliariaAPI`.
2. Crear/configurar la base de datos MySQL apuntada por `ConnectionStrings:DefaultConnection`.
3. Restaurar dependencias y compilar:
    ```bash
    dotnet restore
    dotnet build
    ```
4. Aplicar migraciones y seed inicial (crea tablas y datos base):
    ```bash
    dotnet ef database update
    ```
5. Ejecutar la API:
    ```bash
    dotnet run
    ```
6. La API quedará disponible en el puerto configurado por `dotnet run`. Por defecto, el proyecto usa `http://localhost:5057`; Swagger se expone en `http://localhost:5057`.

## 🐳 Ejecución con Docker Compose

1. Asegurarse de que `docker` y `docker compose` estén instalados.
2. Desde `InmobiliariaAPI`, levantar los servicios:
    ```bash
    docker compose up -d --build
    ```
    - Levanta MySQL (`inmobiliaria_mysql`) y la API (`inmobiliaria_api`).
    - Los datos persisten en el volumen `inmobiliariaapi_mysql_data`.
    - Para logs e imágenes se montan las carpetas `./logs` y `./wwwroot/uploads`.
3. Detener los servicios:
    ```bash
    docker compose down
    ```
    (agregar `-v` si querés borrar el volumen de datos).

## 🗂️ Migraciones y datos semilla

-   Las migraciones iniciales ya están incluidas en el repositorio. Si necesitás ejecutar el seed/base de datos desde cero:
    ```bash
    dotnet ef database update
    ```

La clase `DataSeeder` se ejecuta en `OnModelCreating`, por lo que cada `database update` aplica automáticamente el seed de propietarios, inmuebles, contratos, imágenes y pagos.

## 📄 Documentación (Swagger)

El proyecto configura Swagger sin prefijo (`RoutePrefix = string.Empty`), por lo que la interfaz queda disponible directamente en la raíz del sitio.

-   **Ejecución local**: `http://localhost:5057` (puerto configurado en `launchSettings.json`/`dotnet watch`).
-   **Docker Compose**: `http://localhost:5000` (mapeo del contenedor `api`).

Desde allí podés probar endpoints autenticados, revisar contratos disponibles y descargar el JSON/OpenAPI.

## 🤝 Desarrollado por

**Claudio Suarez**

-   Email: csuarez689@gmail.com
-   GitHub: [csuarez689](https://github.com/csuarez689)
-   LinkedIn: [claudio-suarez](https://www.linkedin.com/in/claudio-suarez)

Proyecto académico para la Universidad de La Punta (ULP)
Materia: Laboratorio de Programación III

## 📄 Licencia

Este proyecto es de uso educativo para la Universidad de La Punta.

## 📁 Carpeta de uploads

Las imágenes se sirven desde `wwwroot/uploads`. En entorno Docker se monta como volumen para persistir archivos.

---

Proyecto académico – Laboratorio de Programación III (ULP).

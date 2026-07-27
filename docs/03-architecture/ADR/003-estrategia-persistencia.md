# ADR 003: Estrategia de Persistencia de Datos

**Fecha:** 2026-07-23
**Estado:** Aceptado

## Contexto
Los datos de deudas, salarios y capacidades de pago son críticos y altamente sensibles. Dado que Avalonia UI permite compilar aplicaciones móviles y de escritorio, se abre la necesidad de soportar arquitecturas "Offline First" (Local) así como arquitecturas "Cloud" centralizadas tradicionales (API).

## Decisión
- **Capa ORM:** Entity Framework Core (EF Core).
- **Motor de Base de Datos (Modo Cloud / API):** PostgreSQL para despliegues en servidores donde múltiples clientes acceden a un backend central.
- **Motor de Base de Datos (Modo Local / Offline):** SQLite, estrictamente cifrado usando la extensión **SQLCipher**, para instalaciones standalone donde el usuario no confía sus datos en la nube.

## Consecuencias
- **Positivas:** La abstracción que provee EF Core permite cambiar el proveedor de base de datos (PostgreSQL <-> SQLite) inyectando distintas configuraciones del `DbContext` según el entorno de compilación, sin alterar el código de aplicación.
- **Negativas:** La configuración y distribución de SQLCipher en C# requiere instalar bibliotecas nativas (`SQLitePCLRaw.bundle_e_sqlcipher`) y manejar el ciclo de vida de la clave de cifrado local de forma muy segura.

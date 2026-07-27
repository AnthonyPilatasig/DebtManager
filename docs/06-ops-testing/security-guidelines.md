# Guía DevSecOps: Gestión de Secretos y Calidad de Código

**Rol Responsable:** `devsecops-github-guardian`

## 1. Regla de Oro
**NUNCA** coloques cadenas de conexión, tokens de API o contraseñas en los archivos `appsettings.json` o `appsettings.Development.json`. Estos archivos son rastreados por Git y cualquier secreto expuesto se considera una brecha grave de seguridad.

## 2. Configuración Local (.NET User Secrets)
Para conectarte a la base de datos de desarrollo (SQLite cifrado), debes usar la bóveda local de secretos de tu sistema operativo.

### Pasos para el Desarrollador:
1. Abre tu terminal.
2. Navega al directorio del API: `cd src/Backend/DebtManager.Api`
3. Ejecuta el siguiente comando para guardar tu contraseña segura:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=app.db;Password=MI_CONTRASENA_SUPER_SECRETA"
   ```

El host web de C# automáticamente leerá esta configuración durante el arranque cuando estemos en entorno `Development`.

## 3. Calidad de Código (Husky y CSharpier)
Hemos integrado `Husky.Net` al ciclo de vida de Git local. Cada vez que el equipo intente hacer un commit (`git commit`), se ejecutará el hook `pre-commit`.
Este hook ejecuta internamente `dotnet csharpier .` para formatear de forma agresiva y automática todo el código C# del repositorio bajo un estándar unificado. Si hay fallos sintácticos, el commit será rechazado hasta que se corrijan.

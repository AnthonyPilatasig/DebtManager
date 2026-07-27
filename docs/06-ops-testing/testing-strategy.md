# Estrategia de Calidad y Pruebas

**Rol Responsable:** `qa-testing-engineer`

## 1. Pruebas de Arquitectura (NetArchTest)
Para blindar el repositorio contra "espagueti code" y acoplamiento accidental, el proyecto `DebtManager.Architecture.Tests` ejecutará reglas estáticas mediante **NetArchTest.eShop**:
- `Domain` **no debe** referenciar a `Application`, `Infrastructure` ni `Api`.
- `Application` **no debe** referenciar a `Infrastructure` ni `Api`.
- Las dependencias hacia fuera (Bases de Datos, APIs externas) se resolverán estrictamente mediante interfaces (Dependency Inversion).
- Los Controladores de la API (`DebtManager.Api`) **no deben** instanciar Repositorios directamente; deben comunicarse enviando un `IRequest` a través de `IMediator`.

## 2. Pruebas Unitarias (xUnit)
- Se aplicarán al proyecto `Domain` y `Application`.
- **Cobertura Mínima:** 85%.
- Se debe testear exhaustivamente la clase de cálculo del **Flujo de Caja Libre (FCL)** y los ordenamientos de las estrategias **Bola de Nieve** y **Avalancha**.

## 3. Pruebas de Integración (Testcontainers)
Para validar la capa `Infrastructure`:
- Se utilizará `Testcontainers` (contenedores Docker efímeros) para levantar una base de datos PostgreSQL real durante la fase de CI.
- Se verificará que las consultas y las migraciones de EF Core se ejecuten con éxito antes del despliegue.

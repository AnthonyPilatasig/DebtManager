# ADR 002: Arquitectura Limpia y CQRS

**Fecha:** 2026-07-23
**Estado:** Aceptado

## Contexto
El cálculo de finanzas (Flujo de Caja, Simulaciones de Desendeudamiento) representa un Core Domain complejo. Si se acopla la lógica matemática de negocio con los accesos a la base de datos o los controladores HTTP, el sistema será monolítico, rígido, y difícil de probar unitariamente.

## Decisión
Se implementará el patrón arquitectónico **Clean Architecture** estructurado rígidamente en 4 capas (Domain, Application, Infrastructure, Api). 
Además, dentro de la capa de Aplicación se integrará el patrón **CQRS (Command Query Responsibility Segregation)** utilizando la librería `MediatR` para C#.

## Consecuencias
- **Positivas:** 
  - Independencia total de frameworks.
  - Reglas de negocio aisladas, puras y 100% testeables en el proyecto `Domain`.
  - Separación clara entre operaciones de mutación de estado (Commands) y de lectura de datos (Queries).
- **Negativas:** 
  - Mayor cantidad de archivos y código boilerplate inicial (DTOs, Handlers, Commands, Queries, Validadores).

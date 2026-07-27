# ADR 004: Estrategia de Sincronización (Offline-First)

**Fecha:** 23 de julio de 2026
**Estado:** Aprobado

## Contexto
El sistema tiene dos componentes que almacenan datos:
1. Una aplicación Frontend (Avalonia) que se ejecuta en los dispositivos de los usuarios y utiliza SQLite local cifrado.
2. Un Backend central (Web API .NET 8) conectado a PostgreSQL.

Dado que la aplicación debe ser funcional sin conexión a internet ("Offline-First"), los usuarios pueden crear o modificar deudas e ingresos localmente. Al recuperar la conexión, estos cambios deben consolidarse con la nube.

## Decisión
Se implementará una arquitectura de sincronización **Push/Pull Diferencial**:
- **Aislamiento Central:** PostgreSQL manejará los datos de todos los usuarios, pero se implementará un `Global Query Filter` en EF Core filtrando por `UserId`.
- **Atributos de Auditoría:** Todas las entidades (heredadas de `Entity.cs`) tendrán los campos `LastModifiedAt` (fecha de modificación) e `IsDeleted` (soft delete).
- **Mecánica Pull:** Al abrir la app, Avalonia llama al endpoint `GET /api/sync?since={last_sync_date}`. El Backend devuelve todos los registros que han sido modificados después de esa fecha. Avalonia hace un UPSERT (Update/Insert) en SQLite.
- **Mecánica Push:** Las transacciones offline locales son registradas en Avalonia y empujadas en lote (`POST /api/sync`) al backend al detectar internet.

## Consecuencias
- (Positivo) La experiencia de usuario no se degrada sin internet.
- (Positivo) Ahorro masivo de ancho de banda al transferir sólo deltas.
- (Negativo) Mayor complejidad en el manejo de conflictos si el usuario modifica los mismos datos en dos dispositivos distintos (se utilizará `LastModifiedAt` como "el más reciente gana" LWW).

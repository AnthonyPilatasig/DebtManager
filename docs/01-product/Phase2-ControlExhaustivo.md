# Fase 2: Control Exhaustivo y Amortización

## Reglas de Negocio Aprobadas
1. **Registro de Cuota (RN-001)**: El sistema permite registrar el pago mensual. Esto marca la notificación del mes actual como "Pagada".
2. **Abonos Extraordinarios (RN-002)**: Se pueden hacer pagos extra (ej. usando un bono/décimo). Los pagos extra reducen el Saldo Total y el Plazo (meses), pero la **cuota mensual se mantiene fija** (estándar bancario).
3. **Plazos Fijos (RN-003)**: El usuario debe indicar el número de cuotas (plazo en meses) al registrar la deuda.
4. **Notificaciones (RN-004)**: El sistema tendrá una bandeja de notificaciones en memoria/BD que alerte sobre los pagos próximos sin llenar el espacio local del dispositivo.

## Entidades de la Base de Datos Añadidas
- `TotalQuotas` en la entidad **Debt**: Número total de cuotas (meses) originalmente pactadas.
- Entidad **Payment**: `Id`, `DebtId`, `Amount`, `Date`, `IsExtraordinary`. Representa una transacción de pago contra una deuda.
- Entidad **Notification**: `Id`, `UserId`, `Message`, `Date`, `IsRead`. Para guardar el historial de alertas a mostrar en la interfaz.

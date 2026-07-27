# Catálogo de Reglas de Negocio y Fórmulas

**Rol Responsable:** `business-analyst-reglas-negocio`

Este documento consolida el Core Domain (Núcleo de Negocio) financiero de la plataforma.

## BR-01: Flujo de Caja Libre (FCL)
Determina el capital real disponible del usuario al final del ciclo de ingresos, tras descontar obligaciones.
> `FCL = Ingresos Totales - (Deudas Fijas Obligatorias + Cuotas Mínimas Variables)`

- **Regla de Bloqueo:** Si `FCL <= 0`, el sistema debe alertar al usuario y bloquear los simuladores de pago acelerado, sugiriendo en su lugar un plan de contingencia (reducción de gastos fijos o reestructuración de deuda).

## BR-02: Semáforo de Riesgo Financiero (Tasa de Endeudamiento - TE)
Calcula el porcentaje de los ingresos comprometido al pago de deudas.
> `TE = (Total Compromisos / Ingresos Totales) * 100`

| Nivel de Riesgo | Rango | Acción de la UI (Avalonia) |
| :--- | :--- | :--- |
| 🟢 **Saludable** | `<= 30%` | Tema Visual Normal. Mensajes positivos de ahorro. |
| 🟡 **Alerta** | `31% - 50%` | Tema Visual Warning. Alertas sobre uso de tarjetas de crédito. |
| 🔴 **Sobreendeudado**| `> 50%` | Tema Visual Error/Critico. Sugerencias urgentes de consolidación. |

## BR-03: Algoritmos de Desendeudamiento Acelerado

### Método 1: Bola de Nieve (Snowball)
Enfocado en el impacto psicológico y victorias rápidas (Quick Wins).
1. Ordenar todas las deudas por **Saldo Total Pendiente** en orden ascendente (de menor a mayor).
2. Asegurar el pago de la Cuota Mínima en todas las obligaciones.
3. Inyectar el 100% del `FCL` a la deuda de menor saldo hasta cancelarla.
4. Una vez cancelada, sumar su antigua cuota mínima al FCL y atacar la siguiente en la lista.

### Método 2: Avalancha (Avalanche)
Enfocado en la eficiencia matemática y el mínimo pago de intereses posibles.
1. Ordenar todas las deudas por **Tasa de Interés Anual (APR/TEA)** en orden descendente (de mayor a menor).
2. Asegurar el pago de la Cuota Mínima en todas las obligaciones.
3. Inyectar el 100% del `FCL` a la deuda con mayor tasa de interés hasta cancelarla.
4. Efecto cascada hacia la siguiente deuda más cara.

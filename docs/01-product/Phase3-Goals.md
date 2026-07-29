# Documento de Producto: Fase 3 (Metas Financieras y Flujo de Caja)

## Visión de la Fase
Transformar **Debt Manager** de un simple gestor de pasivos a un **Asesor Financiero Real**. El objetivo principal es responder a la pregunta del usuario: *"Quiero comprar X, ¿me alcanza el sueldo?"* de manera matemática, realista y cruda.

## Reglas de Negocio Confirmadas

### 1. Cálculo del Flujo de Caja Libre (Free Cash Flow)
Para que el sistema no mienta sobre el dinero que "sobra", no basta con restar `Deudas` de `Ingresos`. Se introduce el concepto de **Gastos Fijos** (Arriendo, Comida, Transporte, Servicios Básicos).

`Flujo de Caja Libre = Total Ingresos Mensuales - (Suma de Cuotas de Deuda + Suma de Gastos Fijos)`

### 2. Entidad: Metas (`Goal`)
Las metas representan objetivos financieros o adquisiciones futuras (ej. Comprar un PS5, un auto, irse de viaje). 
Toda meta tiene:
*   `Nombre`: Qué quiero comprar.
*   `Costo Total`: Cuánto cuesta.
*   `Tipo`: Ahorro o Crédito.
*   `Fecha Objetivo`: Cuándo lo quiero.
*   `Cuota Estimada`: En caso de ser crédito, cuánto pagaría al mes.

### 3. Evaluación de Viabilidad ("¿Me alcanza?")
El sistema evalúa la meta dependiendo de su tipo:

*   **Si es Ahorro (Comprar de contado en X meses):**
    El sistema calcula: `Cuota de Ahorro Mensual = Costo Total / Meses Restantes`.
    **Viable si:** `Flujo de Caja Libre >= Cuota de Ahorro Mensual`.
*   **Si es a Crédito (Comprar hoy, pagar en cuotas):**
    **Viable si:** `Flujo de Caja Libre >= Cuota Estimada`. 
    *(Alerta Roja de Sinceridad si el margen de sobra es menor al 10% del ingreso total, indicando riesgo crítico de liquidez).*

## Impacto en la Arquitectura
*   **Base de datos:** Nuevas tablas `FixedExpenses` y `Goals`.
*   **Backend:** Nuevos dominios y endpoints CQRS. Nuevo servicio lógico `FinancialAdvisorQuery` que junta Ingresos, Deudas y Gastos Fijos para determinar la viabilidad matemática.
*   **Frontend:** Interfaz de semáforo (Verde, Amarillo, Rojo) en la sección de "Asesor Financiero" para dar un veredicto visual rápido.

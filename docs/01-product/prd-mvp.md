# Product Requirements Document (PRD) - MVP v1.0

**Producto:** Plataforma de Gestión de Sueldos y Deudas Personales
**Estado:** Aprobado

## 1. Objetivo del Producto
Ayudar a los usuarios a administrar sus ingresos frente a compromisos financieros, ofreciendo diagnóstico automático y planes de desendeudamiento interactivos.

## 2. Matriz MoSCoW (MVP)

### Must Have (Crítico)
- Gestión de perfiles y autenticación local segura.
- Registro de Ingresos y Deudas (fijas y variables).
- Motor de cálculo de Flujo de Caja Libre (FCL).
- Semáforo de Riesgo Financiero.
- Simuladores de pago algorítmicos: Bola de Nieve y Avalancha.

### Should Have (Importante)
- Dashboard visual (distribución de gastos en gráficos).
- Alertas in-app de vencimientos de servicios.

### Could Have (Deseable)
- Notificaciones vía email (SendGrid/Resend) previas a vencimientos de servicios básicos (agua, luz).
- Integración con API de tipo de cambio.

### Won't Have (Descartado para v1.0)
- Conexión bancaria automática (Open Banking).
- App móvil nativa pura (Se utilizará el framework multiplataforma Avalonia UI para solventarlo).

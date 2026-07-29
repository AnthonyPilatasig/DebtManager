<div align="center">

# 💸 Debt Manager & Financial Advisor
### Tu Asesor Financiero Personal Multiplataforma

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![AvaloniaUI](https://img.shields.io/badge/Avalonia-11.1-purple?style=flat&logo=avalonia&logoColor=white)](https://avaloniaui.net/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-316192?style=flat&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

</div>

---

**Debt Manager** no es solo una lista de deudas; es tu **Asesor Financiero Personal**. Diseñada para darte una visión sincera y realista de tus finanzas, esta aplicación multiplataforma te ayuda a controlar tu flujo de caja, gestionar tus pagos y descubrir si realmente puedes permitirte esa nueva compra sin asfixiarte financieramente.

## ✨ Características Principales

*   **📊 Dashboard Inteligente:** Visualiza de un vistazo tu salud financiera (Ingresos vs Deudas).
*   **💳 Control Exhaustivo de Deudas:** Registra créditos, calcula amortizaciones y controla tus fechas de corte.
*   **💰 Abonos y Cuotas:** Soporte para abonos extraordinarios (ej. Décimos) calculando la reducción real del plazo.
*   **🎯 Metas y Capacidad de Endeudamiento (Fase 3):** Pregúntale a la app: *"¿Me alcanza para comprar esto?"* y recibe una respuesta sincera basada en tu Flujo de Caja Libre.
*   **🔔 Centro de Notificaciones:** Alertas proactivas de próximos pagos para que nunca pagues intereses por mora.
*   **📱 Diseño Multiplataforma:** Interfaz "Liquid Glass" fluida y responsiva que funciona perfecto en PC, Web o Móvil.

---

## 🏗️ Arquitectura del Sistema

El proyecto está construido bajo los principios de **Clean Architecture** y **CQRS**, asegurando un código mantenible, escalable y testable.

### Backend (`DebtManager.Api`)
- **Framework:** ASP.NET Core 9 Minimal APIs.
- **Arquitectura:** Clean Architecture (Domain, Application, Infrastructure, Api).
- **Patrones:** CQRS implementado con **MediatR**.
- **Base de Datos:** PostgreSQL administrada mediante **Entity Framework Core 9**.
- **Documentación API:** Swagger / OpenAPI nativo de .NET 9.

### Frontend (`DebtManager.Frontend`)
- **Framework:** Avalonia UI (v11.1) multiplataforma.
- **Patrón UI:** MVVM (Model-View-ViewModel) usando **CommunityToolkit.Mvvm**.
- **Estilo:** Diseño Responsivo, tipografía Inter, y micro-interacciones.

---

## 🚀 Guía de Instalación Rápida

### Requisitos Previos
1. [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado.
2. Servidor PostgreSQL corriendo (o actualizar la cadena de conexión en `appsettings.json` a SQLite/SQLServer según prefieras).

### Levantar el Backend (API)
```bash
cd src/Backend/DebtManager.Api
dotnet run --launch-profile https
```
> La API inyectará automáticamente las tablas necesarias en la base de datos gracias a las migraciones de EF Core.

### Levantar el Frontend (Aplicación UI)
```bash
cd src/Frontend/DebtManager.Frontend
dotnet run
```

---

## 🗺️ Mapa de Ruta (Roadmap)

- [x] **Fase 1: MVP Básico** - Registro de ingresos y deudas simples, arquitectura Clean + MVVM.
- [x] **Fase 2: Control Exhaustivo** - Registro de abonos, recálculo de cuotas y notificaciones de vencimiento.
- [ ] **Fase 3: Asesor Financiero (Actual)** - Registro de Gastos Fijos, Metas de Ahorro y motor de viabilidad financiera (Capacidad de endeudamiento).
- [ ] **Fase 4: Multiplataforma Real** - Empaquetado para Android/iOS usando Avalonia Mobile y Notificaciones Push nativas.

---

<div align="center">
Desarrollado con ❤️ aplicando las mejores prácticas de la industria de software.
</div>

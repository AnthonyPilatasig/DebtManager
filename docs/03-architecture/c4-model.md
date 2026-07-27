# Diagramas de Arquitectura (C4 Model)

Este documento centraliza los diagramas C4 usando la sintaxis de Mermaid para integrarse nativamente en repositorios de GitHub/GitLab.

## Nivel 1: Diagrama de Contexto
```mermaid
C4Context
    title Diagrama de Contexto - Plataforma Debt Manager
    Person(user, "Usuario Financiero", "Usuario final que desea organizar y saldar sus deudas.")
    System(debtSystem, "Debt Manager System", "Gestiona perfiles, calcula FCL y simula estrategias de desendeudamiento.")
    System_Ext(currencyApi, "Exchange Rates API", "Provee tasas de cambio diarias de monedas.")
    System_Ext(mailApi, "Email Provider", "Gateway de notificaciones (ej. SendGrid/Resend).")

    Rel(user, debtSystem, "Registra datos financieros y visualiza su plan", "HTTPS")
    Rel(debtSystem, currencyApi, "Consulta tasas FX", "HTTPS/JSON")
    Rel(debtSystem, mailApi, "Envía emails transaccionales", "HTTPS/JSON")
```

## Nivel 2: Diagrama de Contenedores
```mermaid
C4Container
    title Diagrama de Contenedores - Arquitectura CSharp y .NET
    Person(user, "Usuario Financiero", "Usuario final")
    
    Container(avaloniaApp, "Frontend App (Avalonia)", "C#, XAML, MVVM", "Aplicación cliente instalable (iOS, Android, Windows) o vía WebAssembly.")
    Container(webApi, "Backend API (.NET 8)", "C#, Web API, CQRS", "Núcleo lógico y servicios expuestos vía endpoints REST.")
    ContainerDb(db, "Base de Datos Cifrada", "PostgreSQL / SQLite (SQLCipher)", "Almacena perfiles, ingresos, deudas y planes.")
    
    Rel(user, avaloniaApp, "Interactúa vía UI nativa")
    Rel(avaloniaApp, webApi, "Ejecuta Commands & Queries", "REST / HTTPS")
    Rel(webApi, db, "Lee y Escribe datos de dominio", "EF Core / TCP")
```

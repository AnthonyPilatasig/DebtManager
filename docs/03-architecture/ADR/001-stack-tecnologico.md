# ADR 001: Selección del Stack Tecnológico Principal

**Fecha:** 2026-07-23
**Estado:** Aceptado

## Contexto
Se requiere construir una plataforma multiplataforma (Web, Desktop, Mobile) para gestión de finanzas personales. El equipo de desarrollo principal tiene experiencia en tecnologías web (React, Angular), pero se busca unificar el lenguaje para reducir la sobrecarga cognitiva en el mantenimiento del código, y maximizar el rendimiento matemático en los algoritmos financieros.

## Decisión
Se ha decidido adoptar un **Stack C# Full-Stack**:
- **Backend:** .NET 8 Web API.
- **Frontend:** Avalonia UI.

## Consecuencias
- **Positivas:** Único lenguaje en todo el repositorio (C#). Alta precisión matemática asegurada por defecto usando el tipo `decimal`. Avalonia UI permite compilar nativamente la interfaz para iOS, Android, macOS, Linux y WebAssembly sin usar puentes interpretados de JavaScript (a diferencia de React Native/Ionic), lo que resulta en alto rendimiento de CPU.
- **Negativas:** Existirá una curva de aprendizaje inicial para transicionar del DOM HTML y Hooks al modelo XAML y el patrón MVVM.

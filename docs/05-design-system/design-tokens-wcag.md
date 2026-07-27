# Sistema de Diseño y Tokens (Avalonia UI)

**Rol Responsable:** `design-tokens-a11y`

## 1. Accesibilidad (WCAG 2.2 AA)
La plataforma trata problemas de estrés financiero, por lo que la carga cognitiva debe ser nula.
Todas las combinaciones de color deben superar obligatoriamente el contraste mínimo de **4.5:1** para texto normal y **3:1** para texto grande (Headers).

## 2. Paleta Semántica
- **Color Primario (Ahorro/Libertad):** `#2E7D32` (Verde Esmeralda). Uso: Botones de confirmación, flujos de ahorro exitosos.
- **Color Secundario (Acción Neutra):** `#1565C0` (Azul Marino). Uso: Controles de navegación y links.
- **Color Crítico (Sobreendeudamiento):** `#D32F2F` (Rojo Alerta). Uso: Advertencias de FCL negativo, cortes de servicio.
- **Color Warning (Alerta Preventiva):** `#F57C00` (Naranja). Uso: Alertas de proximidad de pago.
- **Fondo (Dark Mode por Defecto):** Superficie `#121212` con elevaciones (Cards) en `#1E1E1E`.

## 3. Patrones de Frontend en Avalonia (MVVM)
- **Views (`.axaml`):** Código de marcado puramente declarativo. **PROHIBIDO** escribir lógica de negocio en el *Code-Behind* (`.axaml.cs`). Solo se permite código que manipule directamente elementos visuales (ej. animaciones).
- **ViewModels:** Toda la lógica de presentación debe residir aquí. Se enlazan a la View mediante `DataContext`. Utilizaremos **ReactiveUI** para el manejo reactivo del estado de la interfaz.

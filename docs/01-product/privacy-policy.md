# Política de Privacidad y Manejo de Datos Sensibles

**Rol Responsable:** `privacidad-legal-compliance`
**Dominio:** Datos Financieros Personales

## 1. Principio de Privacidad por Diseño
El sistema maneja datos financieros extremadamente sensibles (salarios, deudas, capacidad de pago, patrimonio). Por diseño, la plataforma debe asegurar que estos datos no queden expuestos ni ante el equipo de desarrollo.

## 2. Estrategia de Almacenamiento (Offline / Local)
Para garantizar la máxima privacidad en el MVP y evitar regulaciones estrictas de cloud computing en etapas tempranas:
- La base de datos local (SQLite) debe estar cifrada utilizando **SQLCipher**.
- La clave maestra de cifrado se derivará del PIN o Contraseña Maestra del usuario utilizando un algoritmo KDF robusto (ej. **Argon2id** o **PBKDF2**).
- El sistema no puede leer la base de datos si el usuario no ha desbloqueado la sesión.

## 3. Telemetría y APIs de Terceros
- **Cero Telemetría Financiera:** Queda estrictamente prohibido enviar montos, salarios o nombres de deudas a sistemas de analytics (Google Analytics, Mixpanel, etc.).
- **Consultas Externas Anónimas:** Al consultar APIs de tipo de cambio (ej. DolarAPI), no se enviarán identificadores de usuario. La plataforma descargará la tasa global y hará la conversión internamente.

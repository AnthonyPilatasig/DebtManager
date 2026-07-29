# Modelo Entidad-Relación (ERD)

Este es el modelo de datos núcleo relacional, optimizado para ser orquestado mediante Entity Framework Core, respetando la 3ra Forma Normal. Incluye las actualizaciones de la Fase 2 (Control Exhaustivo) y Fase 3 (Asesor Financiero).

```mermaid
erDiagram
    USUARIO {
        uuid Id PK
        string Email
        string BaseCurrency
    }
    
    INGRESO {
        uuid Id PK
        uuid UserId FK
        string Description
        decimal Amount
        int Type "0=Fixed, 1=Variable"
    }
    
    DEUDA {
        uuid Id PK
        uuid UserId FK
        string Name
        decimal TotalBalance
        decimal MinimumMonthlyPayment
        decimal AnnualInterestRate
        int DueDay
        bool IsCreditCard
        int CutoffDay
        int TotalQuotas
        datetime StartDate
    }

    PAGO {
        uuid Id PK
        uuid DebtId FK
        decimal Amount
        datetime PaymentDate
        bool IsExtraordinary
    }

    NOTIFICACION {
        uuid Id PK
        uuid UserId FK
        string Message
        bool IsRead
    }

    GASTO_FIJO {
        uuid Id PK
        uuid UserId FK
        string Name
        decimal Amount
        int DueDay
    }

    META {
        uuid Id PK
        uuid UserId FK
        string Name
        int Type "0=Savings, 1=Credit"
        decimal TargetAmount
        datetime TargetDate
        decimal EstimatedMonthlyPayment
    }

    %% Relaciones Principales
    USUARIO ||--o{ INGRESO : registra
    USUARIO ||--o{ DEUDA : debe
    USUARIO ||--o{ NOTIFICACION : recibe
    USUARIO ||--o{ GASTO_FIJO : tiene
    USUARIO ||--o{ META : planifica
    DEUDA ||--o{ PAGO : abona
```

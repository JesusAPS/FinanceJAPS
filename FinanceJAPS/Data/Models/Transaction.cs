using SQLite;

public class Transaction
{
    [PrimaryKey, AutoIncrement]
    public int TransactionID { get; set; } // Identificador único

    [Indexed, NotNull]
    public int UserID { get; set; } // Clave foránea para la tabla Users

    [Indexed, NotNull]
    public int CategoryID { get; set; } // Clave foránea para la tabla Category

    [NotNull]
    public decimal Amount { get; set; } // Monto de la transacción

    [NotNull]
    public DateTime Date { get; set; } = DateTime.UtcNow; // Fecha de la transacción en UTC

    [MaxLength(200)]
    public string Description { get; set; } // Descripción de la transacción

    public TransactionType Type { get; set; } // Tipo de transacción (Ingreso/Gasto)

    [Ignore]
    public User User { get; set; } // Relación con User

    [Ignore]
    public Category Category { get; set; } // Relación con Category
}

public enum TransactionType
{
    Income,
    Expense
}


using SQLite;

public class Budget
{
    [PrimaryKey, AutoIncrement]
    public int BudgetID { get; set; } // Identificador único

    [Indexed, NotNull]
    public int UserID { get; set; } // Clave foránea para la tabla Users

    [Indexed, NotNull]
    public int CategoryID { get; set; } // Clave foránea para la tabla Category

    [NotNull]
    public decimal TotalBudget { get; set; } // Monto total asignado

    [NotNull]
    public decimal Spent { get; set; } // Monto gastado

    [NotNull]
    public DateTime StartDate { get; set; } // Fecha de inicio

    [NotNull]
    public DateTime EndDate { get; set; } // Fecha de fin

    [Ignore]
    public decimal Remaining => TotalBudget - Spent; // Dinero restante (calculado)

    [Ignore]
    public User User { get; set; } // Relación con User

    [Ignore]
    public Category Category { get; set; } // Relación con Category
}



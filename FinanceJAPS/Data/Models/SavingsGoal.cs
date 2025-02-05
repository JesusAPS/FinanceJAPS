using SQLite;

public class SavingsGoal
{
    [PrimaryKey, AutoIncrement]
    public int SavingsGoalID { get; set; } // Identificador único

    [Indexed, NotNull]
    public int UserID { get; set; } // Clave foránea para la tabla Users

    [MaxLength(100), NotNull]
    public string Name { get; set; } // Nombre del objetivo de ahorro

    [NotNull]
    public decimal AmountGoal { get; set; } // Monto objetivo

    [NotNull]
    public decimal TotalAmount { get; set; } // Monto actual ahorrado

    [NotNull]
    public DateTime DateLimit { get; set; } // Fecha límite

    [Ignore]
    public decimal Progress => (TotalAmount / AmountGoal) * 100; // Progreso hacia el objetivo (calculado)

    [Ignore]
    public User User { get; set; } // Relación con User
}
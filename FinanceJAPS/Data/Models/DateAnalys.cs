using SQLite;

public class DateAnalysis
{
    [PrimaryKey, AutoIncrement]
    public int AnalysisID { get; set; } // Identificador único

    [Indexed, NotNull]
    public int UserID { get; set; } // Clave foránea para la tabla Users

    [NotNull]
    public DateTime Month { get; set; } // Mes de análisis

    [NotNull]
    public decimal TotalIncome { get; set; } // Ingresos totales

    [NotNull]
    public decimal TotalExpenses { get; set; } // Gastos totales

    [NotNull]
    public decimal Savings { get; set; } // Ahorros

    [Ignore]
    public decimal NetSavings => TotalIncome - TotalExpenses - Savings; // Ahorro neto (calculado)

    [Ignore]
    public User User { get; set; } // Relación con User
}

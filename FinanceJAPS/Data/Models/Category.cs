using SQLite;
public class Category
{
    [PrimaryKey, AutoIncrement]
    public int CategoryID { get; set; } // Identificador único

    [MaxLength(100), NotNull]
    public string CategoryName { get; set; } // Nombre de la categoría

    public CategoryType Type { get; set; } // Tipo de categoría (Ingreso/Gasto)

    [Ignore]
    public List<Budget> Budgets { get; set; } // Relación con Budgets

    [Ignore]
    public List<Transaction> Transactions { get; set; } // Relación con Transactions
}

public enum CategoryType
{
    Income,
    Expense
}


using SQLite;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int UserID { get; set; } // Identificador único

    [MaxLength(100), NotNull]
    public string Name { get; set; } // Nombre

    [Unique, MaxLength(100), NotNull]
    public string Email { get; set; } // Email único

    [MaxLength(64), NotNull] // Longitud típica para un hash SHA-256
    public string PasswordHash { get; set; } // Contraseña cifrada

    public DateTime DateCreated { get; set; } = DateTime.UtcNow; // Fecha de creación en UTC

    [Ignore]
    public List<Budget> Budgets { get; set; } // Relación con Budgets (no se almacena en la base de datos)
}



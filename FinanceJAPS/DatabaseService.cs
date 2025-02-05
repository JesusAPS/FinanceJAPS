using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        // Asegurar que la ruta de la base de datos sea correcta
        var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbPath);
        Console.WriteLine($"Ruta de la base de datos: {databasePath}");

        // Configurar la conexión a la base de datos
        _database = new SQLiteAsyncConnection(databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

        // Crear tablas de manera asíncrona
        InitializeDatabaseAsync().Wait();
    }

    private async Task InitializeDatabaseAsync()
    {
        try
        {
            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Category>();
            await _database.CreateTableAsync<Budget>();
            await _database.CreateTableAsync<Transaction>();
            await _database.CreateTableAsync<DateAnalysis>();
            await _database.CreateTableAsync<SavingsGoal>();
            Console.WriteLine("Base de datos inicializada correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
        }
    }

    // Métodos para User
    public async Task<bool> InsertUserAsync(User user)
    {
        try
        {
            var existingUser = await _database.Table<User>().FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                Console.WriteLine("El correo ya está en uso.");
                return false;
            }

            await _database.InsertAsync(user);
            Console.WriteLine($"Usuario registrado: {user.Email}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar usuario: {ex.Message}");
            return false;
        }
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        try
        {
            return await _database.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al buscar usuario por email: {ex.Message}");
            return null;
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _database.Table<User>().ToListAsync();
    }

    // Métodos para Budget
    public async Task<Budget> GetBudgetByUserIdAsync(int userId)
    {
        return await _database.Table<Budget>().FirstOrDefaultAsync(b => b.UserID == userId);
    }

    public async Task UpdateBudgetAsync(Budget budget)
    {
        try
        {
            var existingBudget = await _database.Table<Budget>()
                .FirstOrDefaultAsync(b => b.UserID == budget.UserID);

            if (existingBudget != null)
            {
                existingBudget.TotalBudget = budget.TotalBudget;
                existingBudget.Spent = budget.Spent;
                await _database.UpdateAsync(existingBudget);
            }
            else
            {
                await _database.InsertAsync(budget);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar presupuesto: {ex.Message}");
        }
    }

    // Métodos para Category
    public async Task InsertCategoryAsync(Category category)
    {
        try
        {
            await _database.InsertAsync(category);
            Console.WriteLine($"Categoría insertada: {category.CategoryName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar categoría: {ex.Message}");
        }
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _database.Table<Category>().ToListAsync();
    }

    // Métodos para Transaction
    public async Task InsertTransactionAsync(Transaction transaction)
    {
        try
        {
            await _database.InsertAsync(transaction);
            Console.WriteLine($"Transacción insertada: {transaction.Description}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar transacción: {ex.Message}");
        }
    }

    public async Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId)
    {
        return await _database.Table<Transaction>().Where(t => t.UserID == userId).ToListAsync();
    }

    // Métodos para SavingsGoal
    public async Task InsertSavingsGoalAsync(SavingsGoal savingsGoal)
    {
        try
        {
            await _database.InsertAsync(savingsGoal);
            Console.WriteLine($"Objetivo de ahorro insertado: {savingsGoal.Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar objetivo de ahorro: {ex.Message}");
        }
    }

    public async Task<List<SavingsGoal>> GetSavingsGoalsByUserIdAsync(int userId)
    {
        return await _database.Table<SavingsGoal>().Where(s => s.UserID == userId).ToListAsync();
    }

    // Métodos para DateAnalysis
    public async Task InsertDateAnalysisAsync(DateAnalysis dateAnalysis)
    {
        try
        {
            await _database.InsertAsync(dateAnalysis);
            Console.WriteLine($"Análisis mensual insertado: {dateAnalysis.Month}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al insertar análisis mensual: {ex.Message}");
        }
    }

    public async Task<List<DateAnalysis>> GetDateAnalysisByUserIdAsync(int userId)
    {
        return await _database.Table<DateAnalysis>().Where(d => d.UserID == userId).ToListAsync();
    }
}


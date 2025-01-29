using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinanceJAPS.Data.Models;

public class DatabaseService
{
    private readonly SQLiteConnection _database;

    public DatabaseService()
    {
        var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "financeJAPS.db3");
        Console.WriteLine($"Ruta de la base de datos: {databasePath}");

        _database = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

        _database.CreateTable<Users>();
        _database.CreateTable<Category>();
        _database.CreateTable<Budget>();
        _database.CreateTable<Transactions>();

        Console.WriteLine($"Base de datos inicializada correctamente.");
    }


    // Agregar Usuarios de manera asíncrona
    public async Task<bool> InsertUserAsync(Users user)
    {
        return await Task.Run(() =>
        {
            try
            {
                var existingUser = _database.Table<Users>().FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    Console.WriteLine("El correo ya está en uso.");
                    return false;
                }

                _database.Insert(user);
                _database.Commit(); // Asegurar que los cambios se guardan

                Console.WriteLine($"Usuario registrado: {user.Email}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar usuario: {ex.Message}");
                return false;
            }
        });
    }


    public Users GetUserByEmail(string email)
    {
        try
        {
            return _database.Table<Users>().FirstOrDefault(u => u.Email == email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al buscar usuario por email: {ex.Message}");
            return null; // Retorna null si ocurre un error
        }
    }


    public List<Users> GetAllUsers()
    {
        return _database.Table<Users>().ToList();
    }

    public async Task<Users> GetUserByEmailAsync(string email)
    {
        return await Task.Run(() =>
        {
            return _database.Table<Users>().FirstOrDefault(u => u.Email == email);
        });
    }


    // Insertar a la Cartera
    // Obtener el presupuesto actual
    public async Task<Budget> GetBudgetAsync()
    {
        return await Task.Run(() =>
        {
            return _database.Table<Budget>().FirstOrDefault();
        });
    }

    // Actualizar o insertar presupuesto
    public async Task UpdateBudgetAsync(Budget budget)
    {
        await Task.Run(() =>
        {
            var existingBudget = _database.Table<Budget>().FirstOrDefault();
            if (existingBudget != null)
            {
                existingBudget.TotalBudget = budget.TotalBudget;
                _database.Update(existingBudget);
            }
            else
            {
                _database.Insert(budget);
            }
        });
    }

}

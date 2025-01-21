using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using FinanceJAPS.Data.Models;

public class DatabaseService
{
    private readonly SQLiteConnection _database;

    public DatabaseService()
{
    // Obtiene la ruta del directorio donde se ejecuta la aplicación
    var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "financeJAPS.db3");

    // Crea una conexión a la base de datos SQLite
    _database = new SQLiteConnection(databasePath);

    // Crea las tablas en la base de datos (si no existen)
    _database.CreateTable<Users>();
    _database.CreateTable<Category>();
    _database.CreateTable<Budget>();
    _database.CreateTable<Transactions>();

     Console.WriteLine($"Base de datos inicializada en: {databasePath}");
}

    //Agregar Usuarios
    internal Task<bool> InsertUserAsync(Users user)
    {
        try
        {
            // Verificar si el correo ya existe
            var existingUser = _database.Table<Users>().FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                Console.WriteLine("El correo ya está en uso.");
                return Task.FromResult(false); // Retorna false si el correo ya existe
            }

            // Inserta el usuario en la base de datos
            _database.Insert(user);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            // Manejo de errores
            Console.WriteLine($"Error al insertar usuario: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    public Task<List<Users>> GetAllUsersAsync()
    {
        return Task.FromResult(_database.Table<Users>().ToList());
    }

    public Task<Users> GetUserByEmailAsync(string email) => Task.FromResult(_database.Table<Users>().FirstOrDefault(u => u.Email == email));

    internal object Table<T>()
    {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;


public class DatabaseService
{
    private readonly SQLiteConnection _database;

    public DatabaseService()
    {
        // Obtiene la ruta para guardar el archivo de la base de datos
        var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "financeApp.db3");

        // Crea una conexión a la base de datos SQLite
        _database = new SQLiteConnection(databasePath);

        // Crea las tablas en la base de datos (si no existen)
        _database.CreateTable<Users>();
        _database.CreateTable<Category>();
        _database.CreateTable<Budget>();
        _database.CreateTable<Transaction>();
    }

    // Métodos para insertar y consultar datos (ejemplificados antes)
}

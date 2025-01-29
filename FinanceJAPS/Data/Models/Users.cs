using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FinanceJAPS.Data.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int UsuarioID { get; set; } // Identificador único para el usuario
        public string Name { get; set; } // Nombre del usuario
        [Unique]
        public  string Email { get; set; } // Email único
        public  string PasswordHash { get; set; } // Contraseña cifrada
        public DateTime DateCreate { get; set; } = DateTime.Now; // Fecha de creación del usuario

        // Relación con Budget
        [Ignore]
        public  List<Budget> Budgets { get; set; } // Lista de presupuestos del usuario
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace FinanceJAPS.Data.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Identificador único para la transacción
        [Indexed]
        public int UsuarioID { get; set; } // Clave foránea para la tabla Users
        [Indexed]
        public int CategoryID { get; set; } // Clave foránea para la tabla Category
        public decimal Amount { get; set; } // Monto de la transacción
        public DateTime Date { get; set; } // Fecha de la transacción
        public string Description { get; set; } // Descripción de la transacción

        // Objetos de navegación
        [Ignore]
        public Users User { get; set; } // Usuario asociado
        [Ignore]
        public Category Category { get; set; } // Categoría asociada
    }
}


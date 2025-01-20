using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FinanceJAPS.Data.Models
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Identificador único para el presupuesto
        [Indexed]
        public int UserID { get; set; } // Clave foránea para la tabla Users
        [Indexed]
        public int CategoryID { get; set; } // Clave foránea para la tabla Category
        public decimal Amount { get; set; } // Monto asignado
        public DateTime Inicio { get; set; } // Fecha de inicio
        public DateTime Fin { get; set; } // Fecha de fin

        // Objetos de navegación
        [Ignore]
        public Users User { get; set; } // Usuario asociado
        [Ignore]
        public Category Category { get; set; } // Categoría asociada
    }
}


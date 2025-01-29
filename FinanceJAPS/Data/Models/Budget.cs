using System;
using SQLite;

namespace FinanceJAPS.Data.Models
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int BudgetID { get; set; } // Identificador único

        [Indexed]
        public int UsuarioID { get; set; } // Clave foránea para la tabla Users

        [Indexed]
        public int CategoryID { get; set; } // Clave foránea para la tabla Category

        public decimal TotalBudget { get; set; } // Monto total asignado

        public decimal Spent { get; set; } // Monto gastado

        public DateTime Inicio { get; set; } // Fecha de inicio del presupuesto

        public DateTime Fin { get; set; } // Fecha de fin del presupuesto

        // Propiedad calculada (no se almacena en BD)
        [Ignore]
        public decimal Remaining => TotalBudget - Spent; // Dinero restante

        // Objetos de navegación (Ignorados en SQLite)
        [Ignore]
        public Users User { get; set; }

        [Ignore]
        public Category Category { get; set; }
    }
}



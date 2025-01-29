using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FinanceJAPS.Data.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int CategoryID { get; set; } // Identificador único para la categoría
        public required string Namecategory { get; set; } // Nombre de la categoría
        [Indexed]
        public required string Type { get; set; } // Tipo: 'Ingreso' o 'Gasto'

        // Relación con Budget y Transaction
        [Ignore]
        public required List<Budget> Budgets { get; set; } // Presupuestos asociados
        [Ignore]
        public required List<Transactions> Transactions { get; set; } // Transacciones asociadas
    }
}


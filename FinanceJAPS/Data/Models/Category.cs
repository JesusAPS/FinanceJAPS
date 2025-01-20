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
        public int Id { get; set; } // Identificador único para la categoría
        public string Name { get; set; } // Nombre de la categoría
        [Indexed]
        public string Type { get; set; } // Tipo: 'Ingreso' o 'Gasto'

        // Relación con Budget y Transaction
        [Ignore]
        public List<Budget> Budgets { get; set; } // Presupuestos asociados
        [Ignore]
        public List<Transaction> Transactions { get; set; } // Transacciones asociadas
    }
}


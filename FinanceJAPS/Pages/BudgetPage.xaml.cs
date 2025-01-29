using FinanceJAPS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FinanceJAPS.Pages
{
    public partial class BudgetPage : ContentPage // Renombrado a BudgetPage
    {
        private readonly DatabaseService _databaseService;

        public BudgetPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadBudgetData(); // Cargar datos al abrir la página

        }

        private async void AddIncome_Clicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(IncomeAmountEntry.Text, out decimal amount) && amount > 0)
            {
                // Obtener el presupuesto actual de la BD
                var currentBudget = await _databaseService.GetBudgetAsync();

                if (currentBudget == null)
                {
                    currentBudget = new Budget { TotalBudget = 0, Spent = 0 };
                }

                // Actualizar el presupuesto con el nuevo ingreso
                currentBudget.TotalBudget += amount;

                // Guardar en la base de datos
                await _databaseService.UpdateBudgetAsync(currentBudget);

                // Actualizar UI
                LoadBudgetData();

                await DisplayAlert("Success", "Income added successfully!", "OK");
                IncomeAmountEntry.Text = ""; // Limpiar campo
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid amount.", "OK");
            }
        }

        private void LoadBudgetData()
        {
            var currentBudget = _databaseService.GetBudgetAsync().Result;

            if (currentBudget != null)
            {
                TotalBudgetLabel.Text = $"${currentBudget.TotalBudget:F2}";
                SpentLabel.Text = $"${currentBudget.Spent:F2}";
                RemainingLabel.Text = $"${(currentBudget.TotalBudget - currentBudget.Spent):F2}";
            }
            else
            {
                TotalBudgetLabel.Text = "$0.00";
                SpentLabel.Text = "$0.00";
                RemainingLabel.Text = "$0.00";
            }
        }

    }
}
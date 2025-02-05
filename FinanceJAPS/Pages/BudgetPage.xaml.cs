using FinanceJAPS.Data.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FinanceJAPS.Pages
{
    public partial class BudgetPage : ContentPage, INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private readonly Users _currentUser;

        private decimal _totalBudget;
        public decimal TotalBudget
        {
            get => _totalBudget;
            set { _totalBudget = value; OnPropertyChanged(); }
        }

        private decimal _spent;
        public decimal Spent
        {
            get => _spent;
            set { _spent = value; OnPropertyChanged(); }
        }

        private decimal _remaining;
        public decimal Remaining
        {
            get => _remaining;
            set { _remaining = value; OnPropertyChanged(); }
        }

        public BudgetPage(DatabaseService databaseService, Users user)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            BindingContext = this; // Vincular la página al contexto de datos
            _ = LoadBudgetData();
        }

        private async void AddIncome_Clicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(IncomeAmountEntry.Text, out decimal amount) && amount > 0)
            {
                var currentBudget = await _databaseService.GetBudgetByUserIdAsync(_currentUser.UsuarioID);

                if (currentBudget == null)
                {
                    // Asegurar que se asigne el UserID cuando se crea un presupuesto nuevo
                    currentBudget = new Budget
                    {
                        UsuarioID = _currentUser.UsuarioID,
                        TotalBudget = 0,
                        Spent = 0
                    };
                }

                currentBudget.TotalBudget += amount;
                await _databaseService.UpdateBudgetAsync(currentBudget);

                // Verificar en la consola si se está guardando el ID del usuario
                Console.WriteLine($"Budget actualizado: ID Usuario = {currentBudget.UsuarioID}, TotalBudget = {currentBudget.TotalBudget}");

                // Asegurar que la UI se actualiza en el hilo principal
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TotalBudget = currentBudget.TotalBudget;
                    Spent = currentBudget.Spent;
                    Remaining = TotalBudget - Spent;

                    OnPropertyChanged(nameof(TotalBudget));
                    OnPropertyChanged(nameof(Spent));
                    OnPropertyChanged(nameof(Remaining));
                });


                await DisplayAlert("Éxito", "Ingreso agregado correctamente.", "OK");
                IncomeAmountEntry.Text = "";
            }
            else
            {
                await DisplayAlert("Error", "Por favor ingresa una cantidad válida.", "OK");
            }
        }


        private async Task LoadBudgetData()
        {
            if (_currentUser == null)
            {
                await DisplayAlert("Error", "El usuario no está autenticado.", "OK");
                return;
            }

            var currentBudget = await _databaseService.GetBudgetByUserIdAsync(_currentUser.UsuarioID);

            if (currentBudget != null)
            {
                Console.WriteLine($"Usuario ID: {_currentUser.UsuarioID}");
                Console.WriteLine($"Presupuesto encontrado - ID: {currentBudget.BudgetID}, UsuarioID: {currentBudget.UsuarioID}, Total: {currentBudget.TotalBudget}");

                TotalBudget = currentBudget.TotalBudget;
                Spent = currentBudget.Spent;
                Remaining = currentBudget.TotalBudget - currentBudget.Spent;

                // Forzar actualización de la UI manualmente
                OnPropertyChanged(nameof(TotalBudget));
                OnPropertyChanged(nameof(Spent));
                OnPropertyChanged(nameof(Remaining));
            }
            else
            {
                Console.WriteLine($"No se encontró un presupuesto para el usuario con ID: {_currentUser.UsuarioID}");
                TotalBudget = 0;
                Spent = 0;
                Remaining = 0;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

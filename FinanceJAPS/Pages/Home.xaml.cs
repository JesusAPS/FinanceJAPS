using System.Collections.ObjectModel;
using FinanceJAPS.Pages;
using SQLite;

namespace FinanceJAPS.Pages
{
    public partial class Home : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Data.Models.Users _currentUser; // 🟢 Guardar el usuario autenticado

        public ObservableCollection<Transaction> Transactions { get; set; }
        public Command<string> NavigateCommand { get; set; }

        public Home(Data.Models.Users user)
        {
            InitializeComponent();

            _currentUser = user; // 🟢 Guardamos el usuario autenticado

            // Inicializar DatabaseService
            _databaseService = new DatabaseService(App.dbPath);

            // Inicializar lista de transacciones de prueba
            Transactions = new ObservableCollection<Transaction>
            {
                new Transaction("Salary", 4000),
                new Transaction("Groceries", -100),
                new Transaction("Electric Bill", -80),
                new Transaction("Subscription", -12.99)
            };

            NavigateCommand = new Command<string>(NavigateToPage);

            // Establecer BindingContext
            BindingContext = this;

            // 🟢 (Opcional) Mostrar nombre del usuario en la UI
            DisplayWelcomeMessage();
        }

        private void DisplayWelcomeMessage()
        {
            DisplayAlert("Bienvenido", $"Hola, {_currentUser.Name}!", "OK");
        }

        private async void NavigateToPage(string pageName)
        {
            try
            {
                Page page = pageName switch
                {
                    "Transactions" => new Transactions(),
                    "Analysis" => new Analisys(),
                    "BudgetPage" => new BudgetPage(_databaseService, _currentUser), // 🟢 Pasar usuario
                    "Settings" => new Settings(),
                    _ => null
                };

                if (page is not null)
                {
                    await Navigation.PushAsync(page);
                }
                else
                {
                    await DisplayAlert("Error", "Page not found", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Navigation Error", $"Failed to navigate: {ex.Message}", "OK");
            }
        }

        private async void OnBudgetTapped(object sender, EventArgs e)
        {
            // Verifica si _currentUser es null antes de navegar
            if (_currentUser != null)
            {
                // Pasa el usuario y el servicio de base de datos correctamente
                await Navigation.PushAsync(new BudgetPage(_databaseService, _currentUser));
            }
            else
            {
                await DisplayAlert("Error", "No se encontró el usuario autenticado.", "OK");
            }
        }
    }

    public class Transaction
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string FormattedAmount => Amount >= 0 ? $"+${Amount:F2}" : $"-${Math.Abs(Amount):F2}";
        public string AmountColor => Amount >= 0 ? "Green" : "Red";

        public Transaction(string name, double amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}





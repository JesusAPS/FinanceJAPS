using System.Collections.ObjectModel;

namespace FinanceJAPS.Pages
{
    public partial class Home : ContentPage
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public Command<string> NavigateCommand { get; set; }

        public Home(Data.Models.Users user)
        {
            InitializeComponent();

            // Inicializar colecciones y comandos
            Transactions = new ObservableCollection<Transaction>
            {
                new Transaction("Salary", 4000),
                new Transaction("Groceries", -100),
                new Transaction("Electric Bill", -80),
                new Transaction("Subscription", -12.99)
            };

            NavigateCommand = new Command<string>(NavigateToPage);

            // Establecer el BindingContext
            BindingContext = this;
        }

        public Home()
        {
        }

        private async void NavigateToPage(string pageName)
        {
            try
            {
                Page page = pageName switch
                {
                    "Home" => new Home(),
                    "Transactions" => new Transactions(),
                    "Analysis" => new Analisys(),
                    "BudgetPage" => new BudgetPage(),
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





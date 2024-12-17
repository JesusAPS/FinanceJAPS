namespace FinanceJAPS.Pages;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();
    }
    private async void NavigateToPage(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not string targetPage || string.IsNullOrEmpty(targetPage))
            return; // Si el parámetro es nulo o no es un string válido, salimos

        Page page = targetPage switch
        {
            "Transactions" => new Transactions(),
            "Analisys" => new Analisys(),
            "Budget" => new Budget(),
            "Settings" => new Settings(),
            _ => throw new ArgumentException($"Página no válida: {targetPage}")
        };

        if (page != null)
        {
            await Navigation.PushAsync(page);
        }
    }

}



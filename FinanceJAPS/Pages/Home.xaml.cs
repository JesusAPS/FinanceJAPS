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
            return; // Si el par�metro es nulo o no es un string v�lido, salimos

        Page page = targetPage switch
        {
            "Transactions" => new Transactions(),
            "Analisys" => new Analisys(),
            "Budget" => new Budget(),
            "Settings" => new Settings(),
            _ => throw new ArgumentException($"P�gina no v�lida: {targetPage}")
        };

        if (page != null)
        {
            await Navigation.PushAsync(page);
        }
    }

}



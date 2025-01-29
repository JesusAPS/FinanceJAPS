using FinanceJAPS.Data.Models; // Espacio de nombres para los modelos de datos
using System.Security.Cryptography; // Espacio de nombres para usar algoritmos de hash
using System.Text; // Espacio de nombres para manipular cadenas
using System.Threading.Tasks;

namespace FinanceJAPS.Pages;
public partial class Login : ContentPage
{
    private readonly DatabaseService _databaseService;

    public Login()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
    }

    private async void LogIn_Clicked(object sender, EventArgs e)
    {
        var email = Username.Text;
        var password = PasswordHash.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos correctamente.", "OK");
            return;
        }

        if (!email.Contains("@") || !email.Contains("."))
        {
            await DisplayAlert("Error", "Por favor, introduce un correo electrónico válido.", "OK");
            return;
        }

        await VerifyPasswordAsync(email, password);
    }

    public async Task VerifyPasswordAsync(string email, string inputPassword)
    {
        try
        {
            var user = await _databaseService.GetUserByEmailAsync(email);

            if (user == null)
            {
                await DisplayAlert("Error", "El usuario no existe.", "OK");
                return;
            }

            string hashedInputPassword = SecurityHelper.HashPassword(inputPassword);

            if (user.PasswordHash == hashedInputPassword)
            {
                await DisplayAlert("Éxito", "Inicio de sesión exitoso.", "OK");
                await Navigation.PushAsync(new Home(user));
            }
            else
            {
                await DisplayAlert("Error", "La contraseña es incorrecta. Intenta de nuevo.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "OK");
        }
    }

    private void SignUp_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Register());
    }
}

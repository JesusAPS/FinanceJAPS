using FinanceJAPS.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace FinanceJAPS.Pages;

public partial class Login : ContentPage
{
    private readonly DatabaseService _databaseService; // Servicio para interactuar con la base de datos

    public Login()
    {
        InitializeComponent();
        _databaseService = new DatabaseService(); // Crea una instancia del servicio de base de datos
    }

    private async void LogIn_Clicked(object sender, EventArgs e)
    {
        var username = Username.Text;
        var password = PasswordHash.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos correctamente.", "OK");
            return;
        }

        await VerifyPasswordAsync(username, password);
    }

    // Método para verificar la contraseña ingresada en el login
    public async Task VerifyPasswordAsync(string email, string inputPassword)
    {
        // Busca al usuario por correo en la base de datos
        var user = await _databaseService.GetUserByEmailAsync(email); // Espera el resultado del método

        if (user == null)
        {
            await DisplayAlert("Error", "El usuario no existe.", "OK");
            return;
        }

        // Genera el hash de la contraseña ingresada
        string hashedInputPassword = Login.HashPassword(inputPassword);

        // Compara el hash generado con el almacenado en la base de datos
        if (user.PasswordHash == hashedInputPassword) // Verifica la propiedad correcta
        {
            // Si coinciden, muestra un mensaje de éxito o continúa con el flujo
            await DisplayAlert("Éxito", "Inicio de sesión exitoso.", "OK");
            await Navigation.PushAsync(new Home());

        }
        else
        {
            await DisplayAlert("Error", "La contraseña es incorrecta. Intenta de nuevo.", "OK");
            return;
        }
    }

    // Método para hash de la contraseña
    private static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        StringBuilder builder = new();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2")); // Formato hexadecimal
        }
        return builder.ToString();
    }

    private void SignUp_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Register());
    }

}


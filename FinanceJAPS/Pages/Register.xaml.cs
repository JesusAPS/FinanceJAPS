using System.Security.Cryptography; // Espacio de nombres para usar algoritmos de hash
using System.Text; // Espacio de nombres para manipular cadenas
using FinanceJAPS.Data.Models; // Espacio de nombres para el modelo de datos de usuarios

namespace FinanceJAPS.Pages
{
    public partial class Register : ContentPage
    {
        private readonly DatabaseService _databaseService; // Servicio para interactuar con la base de datos

        public Register(DatabaseService databaseService )
        {
            InitializeComponent(); // Inicializa los componentes de la p�gina
            _databaseService = databaseService;
        }

     
        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            // Obtener los datos ingresados por el usuario
            var name = FullName.Text; // Nombre completo
            var email = Email.Text; // Correo electr�nico
            var password = Password.Text; // Contrase�a
            var confirmPassword = ConfirmPassword.Text; // Confirmaci�n de la contrase�a

            // Validaciones de los campos
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                // Muestra un mensaje de error si alg�n campo est� vac�o
                await DisplayAlert("Error", "Por favor, completa todos los campos correctamente.", "OK");
                return; // Sale del m�todo
            }

            if (password != confirmPassword)
            {
                // Muestra un mensaje de error si las contrase�as no coinciden
                await DisplayAlert("Error", "Las contrase�as no coinciden. Intenta de nuevo.", "OK");
                return; // Sale del m�todo
            }

            // Crear un nuevo objeto de usuario
            var user = new Users
            {
                Name = name, // Asigna el nombre
                Email = email, // Asigna el correo
                PasswordHash = HashPassword(password), // Asigna el hash de la contrase�a
            };

            // Guardar el usuario en la base de datos
            var result = await _databaseService.InsertUserAsync(user);
            if (result)
            {
                // Si la inserci�n fue exitosa
                await DisplayAlert("�xito", "Registro completado.", "OK");

                // Esperar 3 segundos antes de redirigir
                await Task.Delay(3000);

                // Navegar a la p�gina de inicio de sesi�n
                await Navigation.PushAsync(new Login());
            }
            else
            {
                // Si el correo ya est� en uso
                await DisplayAlert("Error", "El correo ya est� en uso. Intenta con otro.", "OK");
            }
        }

        // M�todo para hash de la contrase�a
        private string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create(); // Crear una instancia de SHA256
                                                   // Convertir la contrase�a a un arreglo de bytes y calcular el hash
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convertir el hash a una cadena hexadecimal
            StringBuilder builder = new();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2")); // Formato hexadecimal
            }
            return builder.ToString(); // Retorna el hash como cadena
        }
    }
}




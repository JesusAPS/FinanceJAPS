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
            InitializeComponent(); // Inicializa los componentes de la página
            _databaseService = databaseService;
        }

     
        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            // Obtener los datos ingresados por el usuario
            var name = FullName.Text; // Nombre completo
            var email = Email.Text; // Correo electrónico
            var password = Password.Text; // Contraseña
            var confirmPassword = ConfirmPassword.Text; // Confirmación de la contraseña

            // Validaciones de los campos
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                // Muestra un mensaje de error si algún campo está vacío
                await DisplayAlert("Error", "Por favor, completa todos los campos correctamente.", "OK");
                return; // Sale del método
            }

            if (password != confirmPassword)
            {
                // Muestra un mensaje de error si las contraseñas no coinciden
                await DisplayAlert("Error", "Las contraseñas no coinciden. Intenta de nuevo.", "OK");
                return; // Sale del método
            }

            // Crear un nuevo objeto de usuario
            var user = new Users
            {
                Name = name, // Asigna el nombre
                Email = email, // Asigna el correo
                PasswordHash = HashPassword(password), // Asigna el hash de la contraseña
            };

            // Guardar el usuario en la base de datos
            var result = await _databaseService.InsertUserAsync(user);
            if (result)
            {
                // Si la inserción fue exitosa
                await DisplayAlert("Éxito", "Registro completado.", "OK");

                // Esperar 3 segundos antes de redirigir
                await Task.Delay(3000);

                // Navegar a la página de inicio de sesión
                await Navigation.PushAsync(new Login());
            }
            else
            {
                // Si el correo ya está en uso
                await DisplayAlert("Error", "El correo ya está en uso. Intenta con otro.", "OK");
            }
        }

        // Método para hash de la contraseña
        private string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create(); // Crear una instancia de SHA256
                                                   // Convertir la contraseña a un arreglo de bytes y calcular el hash
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




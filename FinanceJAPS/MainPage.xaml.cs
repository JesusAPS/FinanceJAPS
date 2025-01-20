namespace FinanceJAPS
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            // Navegar a la pagina de Registro
            await Navigation.PushAsync(new Pages.Login());
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            // Navegar a la pagina de Registro
            await Navigation.PushAsync(new Pages.Register());
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            // Navegar a la página de recuperación de contraseña
            await Navigation.PushAsync(new Pages.ForgotPassword());
        }
    }
}


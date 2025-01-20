namespace FinanceJAPS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establecer SplashPage como la primera pantalla
            MainPage = new NavigationPage(new SplashPage());
        }


        public static string ConnectionString ="Server=localhost;Database=FinanceJAPS;User=root;Password=g2n9n9n4;";

    }

}

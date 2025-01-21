namespace FinanceJAPS
{
    public partial class App : Application
    {
        public static DatabaseService? DataBase {  get; private set; }
        public static string ConnectionString { get => connectionString; set => connectionString = value; }

        public App()
        {
            InitializeComponent();

            //Inicializar Servicio de Base de Datos
            DatabaseService databaseService = new();
            DataBase = databaseService;

            // Establecer SplashPage como la primera pantalla
            MainPage = new NavigationPage(new SplashPage());
        }

        private const string V = "Server=localhost;Database=FinanceJAPS;User=root;Password=g2n9n9n4;";
        private static string connectionString = V;

    }

}

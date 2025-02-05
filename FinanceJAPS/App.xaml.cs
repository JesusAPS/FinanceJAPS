namespace FinanceJAPS
{
    public partial class App : Application
    {
        public static DatabaseService? DataBase { get; private set; }

        public static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "financeJAPS.db3");

        public App()
        {
            InitializeComponent();

            // Inicializar Servicio de Base de Datos con la ruta correcta
            DataBase = new DatabaseService(dbPath);

            // Establecer SplashPage como la primera pantalla
            MainPage = new NavigationPage(new SplashPage());
        }
    }
}

namespace FinanceJAPS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static string ConnectionString ="Server=localhost;Database=FinanceJAPS;User=root;Password=g2n9n9n4;";

    }

}

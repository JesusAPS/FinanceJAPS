namespace FinanceJAPS;

public partial class SplashPage : ContentPage
{
	public SplashPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        Task.Delay(2000).ContinueWith(_ =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
            });
        });
    }

}
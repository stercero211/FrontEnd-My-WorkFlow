namespace WorkFlow;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Envolver MainPage en NavigationPage para permitir la navegación entre pantallas
        MainPage = new NavigationPage(new MainPage());
    }
}
using System.Windows;
using ProjectManager.App.Pages;

namespace ProjectManager.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new AuthPage());
    }
}
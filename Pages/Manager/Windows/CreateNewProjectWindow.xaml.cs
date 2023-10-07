using System.Windows;
using ProjectManager.App.Helpers;
using ProjectManager.App.Services.Interfaces;

namespace ProjectManager.App.Pages.Manager.Windows;

public partial class CreateNewProjectWindow : Window
{
    private readonly IProjectService _projectService;

    public CreateNewProjectWindow()
    {
        _projectService = AppContainer.Resolve<IProjectService>();

        InitializeComponent();
    }

    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        CreationDataClear();
    }

    private void CreationDataClear()
    {
        TitleTextBox.Clear();
        DescriptionTextBox.Clear();
        DeadlineDatePicker.SelectedDate = null;
    }

    private async void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        SubmitButton.IsEnabled = false;

        var isCreated = await _projectService.CreateProjectAsync(TitleTextBox.Text, DescriptionTextBox.Text,
            DeadlineDatePicker.SelectedDate);
        SubmitButton.IsEnabled = true;

        if (isCreated)
        {
            CreationDataClear();
            MessageBox.Show("Проект создан");
            return;
        }

        MessageBox.Show("Ошибка создания данных");
    }
}
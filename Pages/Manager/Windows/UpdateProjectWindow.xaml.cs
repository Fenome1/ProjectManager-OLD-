using System.Windows;
using ProjectManager.App.Models;
using ProjectManager.App.Services.Interfaces;

namespace ProjectManager.App.Pages.Manager.Windows;

public partial class UpdateProjectWindow : Window
{
    private readonly Project _project;
    private readonly IProjectService _projectService;

    public UpdateProjectWindow(Project project, IProjectService projectService)
    {
        _project = project;
        _projectService = projectService;

        InitializeComponent();
    }

    private void UpdateProjectWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        PlaceTextToBoxes();
    }

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        PlaceTextToBoxes();
    }

    private async void SubmitUpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        SubmitButton.IsEnabled = false;

        var isUpdated = await _projectService.UpdateProjectAsync(_project.IdProject, TitleTextBox.Text,
            DescriptionTextBox.Text,
            DeadlineDatePicker.SelectedDate);

        SubmitButton.IsEnabled = true;

        if (isUpdated)
        {
            MessageBox.Show("Данные обновлены");
            return;
        }

        MessageBox.Show("Ошибка обновления данных");
    }

    private void PlaceTextToBoxes()
    {
        TitleTextBox.Text = _project.Title;
        DescriptionTextBox.Text = _project.Description;
        DeadlineDatePicker.SelectedDate = _project.DeadlineDate;
    }
}
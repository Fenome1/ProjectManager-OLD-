using System.Windows;

namespace ProjectManager.App.Helpers;

public static class MessageBoxHelper
{
    public static bool QuestionMessageBoxShow(string question, string caption)
    {
        var result = MessageBox.Show(question, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        return result != MessageBoxResult.Yes;
    }
}
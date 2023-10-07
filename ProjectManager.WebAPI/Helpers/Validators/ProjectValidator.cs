
using ProjectManager.WebAPI.Requests.Projects;

namespace ProjectManager.WebAPI.Helpers.Validators;

public static class ProjectValidator
{
    public static void Validate(CreateProjectRequest request)
    {
        if(string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Название проекта должено содержать хотя бы 1 символ");
    }
    public static void Validate(ResetUserAssignToProject request)
    {
        if ((request.idStatus > 3 && request.idStatus < 0))
            throw new ArgumentException("Задан не корректный статус проекта");
    }
}
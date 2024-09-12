namespace Infrastructure.Common;

public static class EmailTemplateHelper
{
    public static string GetTemplate(string maitoGet)
    {
        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates", (maitoGet + ".html"));

            return File.ReadAllText(path);
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }
}

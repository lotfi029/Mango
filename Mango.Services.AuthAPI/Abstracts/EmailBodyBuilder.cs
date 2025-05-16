namespace Store.Services.AuthAPI.Abstracts;

public class EmailBodyBuilder
{
    public static string GenerateEmailBody(string template, Dictionary<string, string> templeteValus)
    {
        var templetePath = $"{Directory.GetCurrentDirectory()}/Temlates/{template}.html";
        var streamReader = new StreamReader(templetePath);
        var body = streamReader.ReadToEnd();
        streamReader.Close();

        foreach (var item in templeteValus)
            body = body.Replace(item.Key, item.Value);

        return body;
    }
}

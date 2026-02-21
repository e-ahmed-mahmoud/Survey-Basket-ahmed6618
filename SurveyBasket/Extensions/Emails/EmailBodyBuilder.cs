using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Extensions.Emails;

public static class EmailBodyBuilder
{
    public static string GenerateEmailBody(string templateName, Dictionary<string, string> templateModel)
    {
        var templatePath = $"{Directory.GetCurrentDirectory()}/Extensions/Emails/Templates/{templateName}.html";

        var streamReader = new StreamReader(templatePath);
        var body = streamReader.ReadToEnd();
        streamReader.Close();
        foreach (var item in templateModel)
            body = body.Replace(item.Key, item.Value);

        return body;
    }

    public static string EmailConfirmationHtmlTemplateName = "email-confirmation-template";
}

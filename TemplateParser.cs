using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Lab5
{
    public class TemplateParser
    {
        public static string ParseTemplate(string templatePath, Dictionary<string, object> replacements)
        {
            string template = File.ReadAllText(templatePath);

            foreach (var replacement in replacements)
            {
                // Перевірка на null і встановлення відображення для null значень
                if (replacement.Value == null)
                {
                    template = template.Replace(replacement.Key, "N/A");
                    continue;
                }

                // Конвертація значень до рядка з особливою обробкою для дат та інших типів
                string replacementString = ConvertValueToString(replacement.Value);
                template = template.Replace(replacement.Key, replacementString);
            }

            return template;
        }

        private static string ConvertValueToString(object value)
        {
            // Особлива обробка для DateTime
            if (value is DateTime dateTimeValue)
            {
                // Формат можна налаштувати
                return dateTimeValue.ToString("dd-MM-yyyy");
            }

            // Особлива обробка для інших типів може бути додана тут

            // За замовчуванням використовуємо ToString()
            return value.ToString();
        }
    }
}
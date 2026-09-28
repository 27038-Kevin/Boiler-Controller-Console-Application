using System.Text;

namespace BoilerController.Utils
{
    internal static class CaseConverter
    {
        internal static string PascalToTitle(string pascalCasedString)
        {
            if (pascalCasedString.Length == 0)
                return pascalCasedString;

            var titleCasedString = new StringBuilder();
            titleCasedString.Append(char.ToUpper(pascalCasedString[0]));

            for (int i = 1; i < pascalCasedString.Length; ++i)
            {
                if (char.IsUpper(pascalCasedString, i))
                    titleCasedString.Append(' ');

                titleCasedString.Append(pascalCasedString[i]);
            }

            return titleCasedString.ToString();
        }
    }
}

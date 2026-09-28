using System.Text;

namespace BoilerController.Utils
{
    /// <summary>
    /// Static utility class that converts one type of case to another.
    /// </summary>
    internal static class CaseConverter
    {
        /// <summary>
        /// Converts PascalCasedStrings to Title Cased Strings.
        /// </summary>
        /// <param name="pascalCasedString">The string to be converted.</param>
        /// <returns>A title-cased string.</returns>
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

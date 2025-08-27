using System.Text;

namespace Banana.Core.Helpers
{
    public class CssStyleBuilder
    {
        private readonly Dictionary<string, string> _styles = [];

        public CssStyleBuilder(string? baseStyle = null)
        {
            if (!string.IsNullOrEmpty(baseStyle))
            {
                string[] styles = baseStyle.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (string style in styles)
                {
                    if (ParseStyle(style, out string key, out string value))
                    {
                        _styles.Add(key, value);
                    }
                }
            }
        }

        public CssStyleBuilder Add(string key, string value)
        {
            if (!_styles.TryAdd(key, value))
            {
                throw new InvalidOperationException($"'{key}' already exists");
            }

            return this;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            foreach (var style in _styles)
            {
                stringBuilder.Append($"{style.Key}:{style.Value};");
            }
            return stringBuilder.ToString();
        }

        private static bool ParseStyle(string style, out string key, out string value)
        {
            string[] elements = style.Split(':', StringSplitOptions.TrimEntries);

            if (elements.Length == 2)
            {
                key = elements[0];
                value = elements[1];
                return true;
            }

            key = string.Empty;
            value = string.Empty;

            return false;
        }
    }
}

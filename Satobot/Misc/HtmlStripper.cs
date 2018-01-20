namespace Satobot.Misc
{
    public static class HtmlStripper
    {
        public static string StripTags(string source)
        {
            var chars = new char[source.Length];
            var index = 0;
            var inside = false;

            foreach (var letter in source)
            {
                switch (letter)
                {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (inside)
                    continue;

                chars[index] = letter;
                index++;
            }

            return new string(chars, 0, index);
        }
    }
}

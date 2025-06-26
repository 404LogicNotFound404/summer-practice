namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            string new_string = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsPunctuation(input[i]) && !char.IsWhiteSpace(input[i])) new_string = new_string + input[i];
            }
            new_string = new_string.ToLower();

            string reversed = new string(new_string.Reverse().ToArray());
            if (new_string == reversed && !string.IsNullOrEmpty(new_string)) return true;
            return false;
        }
    }
}

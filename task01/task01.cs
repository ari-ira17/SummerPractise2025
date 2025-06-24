namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (input == null || input == "")
        {
            return false;
        }
        else
        {
            string line = "";

            foreach (char ch in input.ToLower())
            {
                if (!char.IsPunctuation(ch) && !char.IsWhiteSpace(ch))
                {
                    line += Convert.ToChar(ch);
                }
            }
            
            for (int i = 0; i < input.Length / 2; i++)
            {
                if (line[i] != line[line.Length - i - 1])
                {
                    return false;
                }
            }

            return true;
        }  
    }
}

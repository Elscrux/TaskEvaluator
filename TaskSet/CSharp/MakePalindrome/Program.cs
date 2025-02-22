namespace Task;

public static class TaskClass {
    /// <summary>
    ///  Find the shortest palindrome that begins with a supplied string. Algorithm idea is simple: - Find the longest postfix of supplied string that is a palindrome. - Append to the end of the string reverse of a string prefix that comes before the palindromic suffix. 
    /// These helper functions are available: bool IsPalindrome(string @string)
    /// </summary>
    public static string MakePalindrome(string @string) {
        //INSERT_CODE_HERE
    }

    /// <summary>
    ///  Checks if given string is a palindrome 
    /// 
    /// </summary>
    public static bool IsPalindrome(string @text) {
        return @text.Equals(new string(@text.ToCharArray().Reverse().ToArray()));
    }
}
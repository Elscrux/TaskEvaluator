namespace Task;

public static class TaskClass {
    /// <summary>
    ///  takes as input string encoded with encode_shift function. Returns decoded string. 
    /// These helper functions are available: string EncodeShift(string @s)
    /// </summary>
    public static string DecodeShift(string @s) {
        //INSERT_CODE_HERE
    }

    /// <summary>
    /// returns encoded string by shifting every character by 5 in the alphabet.
    /// </summary>
    public static string EncodeShift(string @s) {
        return string.Join(string.Empty, s.Select(ch => (char)(((ch + 5 - 'a') % 26) + 'a')));
    }
}
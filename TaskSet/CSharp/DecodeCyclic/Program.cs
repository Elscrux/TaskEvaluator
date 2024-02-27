namespace Task;

public static class TaskClass {
    /// <summary>
    ///  takes as input string encoded with encode_cyclic function. Returns decoded string. 
    /// These helper functions are available: string EncodeCyclic(string @s)
    /// </summary>
    public static string DecodeCyclic(string @s) {
        //INSERT_CODE_HERE
    }

    /// <summary>
    /// returns encoded string by cycling groups of three characters.
    /// split string to groups. Each of length 3.
    /// </summary>
    public static string EncodeCyclic(string @s) {
        // groups = [s[(3 * i):min((3 * i + 3), len(s))] for i in range((len(s) + 2) // 3)]
        //
        //     # cycle elements in each group. Unless group has fewer elements than 3.
        //     groups = [(group[1:] + group[0]) if len(group) == 3 else group for group in groups]
        //
        //     return \"\".join(groups)
        
        var groups = Enumerable.Range(0, (s.Length + 2) / 3).Select(i => s.Substring(3 * i, Math.Min(3 * i + 3, s.Length))).ToList();
        // cycle elements in each group. Unless group has fewer elements than 3.
        groups = groups.Select(group => group.Length == 3 ? group[1..] + group[0] : group).ToList();
        return string.Join("", groups);
    }
}
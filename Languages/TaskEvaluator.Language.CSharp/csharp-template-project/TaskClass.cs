namespace Task;

public static class TaskClass {
    public static int GetWeekday(int year, int month, int day) {
    var date = new DateTime(year, month, day);
    return date.DayOfWeek switch {
        DayOfWeek.Monday => 0,
        DayOfWeek.Tuesday => 1,
        DayOfWeek.Wednesday => 2,
        DayOfWeek.Thursday => 3,
        DayOfWeek.Friday => 4,
        DayOfWeek.Saturday => 5,
        DayOfWeek.Sunday => 6,
        _ => throw new ArgumentOutOfRangeException(nameof(date.DayOfWeek))
    };
}
}
WriteTimeZones();

WriteConvertedTimeZones("UTC-09");

Console.ReadLine();

static void WriteTimeZones()
{
    Console.WriteLine($"{"ID",-40} | {"StandartName",-40}\n");
    foreach (var z in TimeZoneInfo.GetSystemTimeZones())
    {
        Console.WriteLine($"{z.Id,-40} | {z.StandardName,-40}");
    }
}

static void WriteConvertedTimeZones(string zoneId)
{
    var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
    var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

    Console.WriteLine($"\n\nBefore: {DateTime.Now} \nAfter: {dateTime}\n\n");
}
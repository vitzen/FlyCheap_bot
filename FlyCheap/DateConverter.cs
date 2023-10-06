using System.Globalization;

namespace FlyCheap;

public class DateConverter
{
    string openTimeFormat = "dd.mm.yyyy";
    DateTime dateTime = DateTime.ParseExact(date, openTimeFormat, CultureInfo.InvariantCulture);

// На случай возобновления летнего времени.
    TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
    var openTime = new DateTimeOffset(dateTime, timeZone.GetUtcOffset(dateTime));
}


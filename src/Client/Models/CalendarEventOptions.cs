using System.Collections.Generic;

namespace Steeltoe.Client.Models
{
    public class CalendarEventOptions
    {
        public List<CalendarEvent> Events { get; set; } = new();
    }
}

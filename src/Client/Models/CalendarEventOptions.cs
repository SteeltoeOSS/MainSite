using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Steeltoe.Client.Models
{
    public class CalendarEventOptions
    {
        public List<CalendarEvent> Events { get; set; } = new();
    }
}

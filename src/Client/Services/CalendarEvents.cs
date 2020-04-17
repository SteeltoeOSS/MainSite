using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

public interface ICalendarEvents {
  Task<CalendarEvent[]> GetEventsAsync();
  void LoadEventsAsync();
}
public class CalendarEvents: ICalendarEvents {
  private HttpClient _http;
  public CalendarEvent[] Events { get; private set; }

  public CalendarEvents(HttpClient httpClient) {
    _http = httpClient;
  }
  public Task<CalendarEvent[]> GetEventsAsync() {
    return _http.GetJsonAsync<CalendarEvent[]>("site-data/CalendarEvents.json");
  }
  public async void LoadEventsAsync() {
    Events = await _http.GetJsonAsync<CalendarEvent[]>("site-data/CalendarEvents.json");
  }
}

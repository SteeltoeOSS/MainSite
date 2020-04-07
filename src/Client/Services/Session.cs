using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Menu;
using Microsoft.AspNetCore.Components;

public class Session {
  private HttpClient _http;

  public Session(HttpClient httpClient) {
    _http = httpClient;
  }

  public List<MenuItem> Version2_MenuItems { get; private set; }
  public List<MenuItem> Version3_MenuItems { get; private set; }
  public CalendarEvent[] CalendarEvents { get; private set; }

  public async Task LoadSiteItemsAsync() {
    Debug.WriteLine("Getting menu items");
    Version2_MenuItems = await _http.GetJsonAsync<List<MenuItem>>("site-data/docs/2/toc.json");
    Version3_MenuItems = await _http.GetJsonAsync<List<MenuItem>>("site-data/docs/3/toc.json");
    Debug.WriteLine("Version 3 count: " + Version3_MenuItems.Count);

    CalendarEvents = await _http.GetJsonAsync<CalendarEvent[]>("site-data/CalendarEvents.json");
  }
}

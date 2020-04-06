using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Menu;
using Microsoft.AspNetCore.Components;

public class Session {
  private int _version = 3;
  private HttpClient _http;

  public Session(HttpClient httpClient) {
    _http = httpClient;
  }

  public int Version { get { return _version; } set { _version = value;  } }

  public List<MenuItem> Version2_MenuItems { get; private set; }
  public List<MenuItem> Version3_MenuItems { get; private set; }

  public async Task LoadTOCAsync() {
    Version2_MenuItems = await _http.GetJsonAsync<List<MenuItem>>("site-data/docs/2/toc.json");
    Version3_MenuItems = await _http.GetJsonAsync<List<MenuItem>>("site-data/docs/3/toc.json");
  }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Menu;
using Microsoft.AspNetCore.Components;

public interface INavMenu {
  Task<MenuItem[]> GetMenuItemsAsync(int versionNumber);
  void LoadMenuItemsAsync();
}
public class NavMenu: INavMenu {
  private HttpClient _http;
  public MenuItem[] MenuItemsV2 { get; private set; }
  public MenuItem[] MenuItemsV3 { get; private set; }

  public NavMenu(HttpClient httpClient) {
    _http = httpClient;
  }
  public Task<MenuItem[]> GetMenuItemsAsync(int versionNumber) {
    return _http.GetJsonAsync<MenuItem[]>("site-data/docs/"+ versionNumber +"/toc.json");
  }
  public async void LoadMenuItemsAsync() {
    MenuItemsV2 = await _http.GetJsonAsync<MenuItem[]>("site-data/docs/2/toc.json");
    MenuItemsV3 = await _http.GetJsonAsync<MenuItem[]>("site-data/docs/3/toc.json");
  }
}

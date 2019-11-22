using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Diagnostics;
using Blazored.Menu;
using System.Threading.Tasks;

namespace Steeltoe.Client.Shared {
	public class DocsMenuBase : ComponentBase {
		[Inject]
		NavigationManager NavigationManager { get; set; }
		[Inject]
		System.Net.Http.HttpClient Http { get; set; }

		protected MenuBuilder MenuBuilder;

		private List<MenuItem> menuItems;

		protected override async Task OnInitializedAsync() {
			NavigationManager.LocationChanged += LocationChanged;
			menuItems = await Http.GetJsonAsync<List<MenuItem>>("site-data/docs/toc.json");
			RenderMenu(NavigationManager.Uri) ;
		}
		protected override void OnAfterRender(bool firstRender) {
			
		}
		private void LocationChanged(object sender, LocationChangedEventArgs e) {
			RenderMenu(e.Location);
			StateHasChanged();
		}

		private void RenderMenu(string location) {
			List<MenuItem> finalItems = new List<MenuItem>();

			if (menuItems == null)
				return;

			foreach (MenuItem menuitem in menuItems) {
				finalItems.Add(menuitem.Clone());

				if (!location.Contains(menuitem.Link)) {
					finalItems[finalItems.Count - 1].MenuItems = new List<MenuItem>();
				}
			}

			MenuBuilder = new MenuBuilder(finalItems);
		}
	}
}

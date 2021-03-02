using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public interface IDocsSite {
	string ApiBrowserHome { get; }
	string BaseAddress { get; }
	string BlogHome { get; }
	string DocsHome { get; }
}

public class DocsSite : IDocsSite {
	private string _baseAddress;

	public DocsSite(IWebAssemblyHostEnvironment HostEnvironment) {
		switch (HostEnvironment.Environment) {
			case ("Development"):
				_baseAddress = "http://localhost:8082";
				break;
			case ("Staging"):
				_baseAddress = "https://docs-dev.steeltoe.io";
				break;
			case ("Production"):
			default:
				_baseAddress = "https://docs.steeltoe.io";
				break;
		}
	}
	public string ApiBrowserHome => $"{_baseAddress}/api/browser/v3/all";
	public string BaseAddress => _baseAddress;
	public string BlogHome => $"{_baseAddress}/articles/";
	public string DocsHome => $"{_baseAddress}/api/v3/welcome/";
}
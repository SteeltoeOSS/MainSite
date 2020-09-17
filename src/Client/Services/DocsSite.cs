using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public interface IDocsSite {
	string BaseAddress { get; }
	string BlogHome { get; }
	string DocsHome { get; }
}

public class DocsSite : IDocsSite {
	private string _baseAddress;

	public DocsSite(IWebAssemblyHostEnvironment HostEnvironment) {
		switch (HostEnvironment.Environment) {
			case ("Development"):
				_baseAddress = "http://localhost:8081";
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
	public string BaseAddress => _baseAddress;
	public string DocsHome => $"{_baseAddress}/api/v3/welcome/whats-new.html";
	public string BlogHome => $"{_baseAddress}/articles/index.html";
}
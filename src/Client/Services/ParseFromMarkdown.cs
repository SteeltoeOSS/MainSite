using Markdig;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

public interface IParseFromMarkdown {
  Task<MarkupString> GetHtmlAsync(string siteDataFilePath);
  void LoadHtmlAsync(string siteDataFilePath);
}
public class ParseFromMarkdown : IParseFromMarkdown {
  private readonly HttpClient _http;

  public MarkupString Html { get; private set; }

  public ParseFromMarkdown(HttpClient httpClient) {
    _http = httpClient;
  }
  public async Task<MarkupString> GetHtmlAsync(string siteDataFilePath) {
    var str = await _http.GetStringAsync($"site-data/{siteDataFilePath}");
    return (MarkupString)BuildHtmlFromMarkdown(str);
  }
  public async void LoadHtmlAsync(string siteDataFilePath) {
    var str = await _http.GetStringAsync($"site-data/{siteDataFilePath}");
    Html = (MarkupString)BuildHtmlFromMarkdown(str);
  }
  private string BuildHtmlFromMarkdown(string value) => Markdig.Markdown.ToHtml(
    markdown: value,
    pipeline: new MarkdownPipelineBuilder().UseGenericAttributes().UseAutoIdentifiers().UseAutoLinks().UseBootstrap().UseAdvancedExtensions().Build()
  );
}

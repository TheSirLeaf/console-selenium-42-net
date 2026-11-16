using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ConsoleSelenium.Tests;

[TestClass]
public sealed class BuscaDesafioTest
{
    private IWebDriver _driver = null!;
    private DuckDuckGoPage _page = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new FirefoxOptions();
        if (Environment.GetEnvironmentVariable("HEADLESS") != "0")
            options.AddArgument("-headless");

        _driver = new FirefoxDriver(options);
        _page = new DuckDuckGoPage(_driver);
    }

    [TestCleanup]
    public void Teardown() => _driver.Quit();

    [TestMethod]
    public void Exercicio1_PesquisaBasica_ValidacaoTitulo()
    {
        const string termo = "Selenium WebDriver C#";

        _page.Open();
        _page.Search(termo);

        var contemTermo = _driver.Title.Contains(termo, StringComparison.OrdinalIgnoreCase)
                          || Uri.UnescapeDataString(_driver.Url).Replace('+', ' ').Contains(termo, StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(contemTermo, $"Título/URL sem o termo. Título: {_driver.Title} | URL: {_driver.Url}");
    }

    [TestMethod]
    public void Exercicio2_Autocompletar_Suggestions()
    {
        _page.Open();
        _page.Type("C# unit testing");

        var sugestoes = _page.WaitForSuggestions(minimum: 3);
        var terceira = sugestoes[2];
        var texto = terceira.Text.Trim();
        terceira.Click();

        _page.WaitForResultsUrl();
        Assert.IsTrue(_driver.Url.Contains("q="), $"URL inesperada: {_driver.Url}");
        Assert.IsFalse(string.IsNullOrEmpty(texto), "Sugestão sem texto.");
    }

    [TestMethod]
    public void Exercicio3_NavegacaoHistorico()
    {
        _page.Open();
        _page.Search("Automated Testing");
        var urlResultados = _driver.Url;

        _page.ClickFirstOrganicResult();
        _page.WaitForExternalPage();
        Assert.IsFalse(_driver.Url.Contains("duckduckgo.com"), $"Não saiu da busca: {_driver.Url}");

        _driver.Navigate().Back();
        _page.WaitForResults();

        Assert.IsTrue(_driver.Url.Contains("duckduckgo.com") && _driver.Url.Contains("q="),
            $"Resultados não restaurados: {_driver.Url}");
        Assert.AreEqual(urlResultados, _driver.Url);
    }
}

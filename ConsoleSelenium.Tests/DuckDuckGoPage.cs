using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConsoleSelenium.Tests;

public sealed class DuckDuckGoPage(IWebDriver driver)
{
    private const string BaseUrl = "https://duckduckgo.com";
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(20);
    private readonly WebDriverWait _wait = new(driver, Timeout);

    public void Open() => driver.Navigate().GoToUrl(BaseUrl);

    public void Type(string term)
    {
        var box = _wait.Until(d => d.FindElement(By.CssSelector("[name='q']")));
        box.Clear();
        box.SendKeys(term);
    }

    public void Search(string term)
    {
        Type(term);
        driver.FindElement(By.CssSelector("[name='q']")).SendKeys(Keys.Enter);
        WaitForResults();
    }

    public void WaitForResults() =>
        _wait.Until(d => d.Url.Contains("q=") && d.FindElements(By.CssSelector("[data-testid='result']")).Count > 0);

    public IReadOnlyList<IWebElement> WaitForSuggestions(int minimum)
    {
        const string selector = ".search-suggestions-list li, .sbox-suggestions li, [role='option']";
        return _wait.Until(d =>
        {
            var items = d.FindElements(By.CssSelector(selector));
            return items.Count >= minimum ? items : null;
        })!;
    }

    public void ClickFirstOrganicResult()
    {
        var link = _wait.Until(d => d.FindElement(By.CssSelector("[data-testid='result-title-a']")));
        link.Click();
    }

    public void WaitForExternalPage() =>
        _wait.Until(d => !d.Url.Contains("duckduckgo.com"));

    public void WaitForResultsUrl() =>
        _wait.Until(d => d.Url.Contains("duckduckgo.com") && d.Url.Contains("q="));
}

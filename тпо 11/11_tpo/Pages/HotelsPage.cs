using OpenQA.Selenium;

public class HotelsPage
{
	private IWebDriver driver;

	public HotelsPage(IWebDriver driver)
	{
		this.driver = driver;
	}

	public void ClickDropdownButton()
	{
		var buttonElement = driver.FindElement(By.XPath("//div[contains(@class, 'o-button') and contains(@class, 'o-dropdown-elementWrapper')]//button[@data-ti='order-dropdown']"));
		buttonElement.Click();
	}

	public void SelectSortOption(string option)
	{
		driver.FindElement(By.XPath($"//div[contains(text(), '{option}')]")).Click();
	}

	public void ClickSearchButton()
	{
		driver.FindElement(By.XPath("//span[text()='Найти']")).Click();
	}

	public void ClickHelpButton()
	{
		driver.FindElement(By.XPath("//span[@class='VFkLz33Grg0Jo3fV' and text()='Справочная']")).Click();
	}

	public void ClickFAQLink()
	{
		driver.FindElement(By.XPath("//a[text()='Частые вопросы про отели']")).Click();
	}

	public void ClickSpecificLink(string linkText)
	{
		var link = driver.FindElement(By.XPath($"//a[@href='https://www.tutu.ru/2read/hotels/hotels_faq/#login']"));
		link.Click();
	}
}
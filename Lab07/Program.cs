using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
	static void Main()
	{
		// Установка пути к драйверу Chrome
		ChromeDriverService service = ChromeDriverService.CreateDefaultService();
		service.HideCommandPromptWindow = true;

		// Инициализация драйвера
		IWebDriver driver = new ChromeDriver(service);

		// Открытие сайта
		driver.Navigate().GoToUrl("https://ru.wikipedia.org/wiki/Booking.com");

		// Поиск элементов разными способами
		// По CSS-селекторам
		IWebElement elementByCss1 = driver.FindElement(By.CssSelector("span.mw-page-title-main"));
		IWebElement elementByCss2 = driver.FindElement(By.CssSelector("th.plainlist")); 
	    IWebElement elementByCss3 = driver.FindElement(By.CssSelector("h2#Примечания"));
	
		// По XPath
		IWebElement elementByXPath1 = driver.FindElement(By.XPath("//a[@class='mw-jump-link']"));
		IWebElement elementByXPath2 = driver.FindElement(By.XPath("//li[@id='footer-info-lastmod']"));
		IWebElement elementByXPath3 = driver.FindElement(By.XPath("//div[@id='siteSub']"));

		// По тегу
		IWebElement elementByTag = driver.FindElement(By.TagName("h1"));

		// По частичному тексту ссылки
		IWebElement elementByPartialLinkText = driver.FindElement(By.PartialLinkText("Амстердам"));

		// Вывод содержимого элементов
		Console.WriteLine("Text of element found by CSS selector: " + elementByCss1.Text);
		Console.WriteLine("Text of element found by CSS selector: " + elementByCss2.Text);
		Console.WriteLine("Text of element found by CSS selector: " + elementByCss3.Text);
		Console.WriteLine("Text of element found by XPath: " + elementByXPath1.Text);
		Console.WriteLine("Text of element found by XPath: " + elementByXPath2.Text);
		Console.WriteLine("Text of element found by XPath: " + elementByXPath3.Text);
		Console.WriteLine("Text of element found by tag name: " + elementByTag.Text);
		Console.WriteLine("Text of element found by partial link text: " + elementByPartialLinkText.Text);

		// Закрытие браузера
		driver.Quit();
	}
}
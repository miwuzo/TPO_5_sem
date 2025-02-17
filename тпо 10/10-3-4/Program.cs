using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

class Program
{
	static void Main(string[] args)
	{
		IWebDriver driver = new ChromeDriver();
		driver.Navigate().GoToUrl("https://hotel.tutu.ru/c_belarus/minsk/");
		Thread.Sleep(2000);
		// АВТОРИЗАЦИЯ
		var loginButton = driver.FindElement(By.XPath("//div[text()='Войти']"));
		loginButton.Click();
		Thread.Sleep(2000);
		var emailField = driver.FindElement(By.Name("email"));
		emailField.SendKeys("soweomi@gmail.com");
		var passwordField = driver.FindElement(By.Name("password"));
		passwordField.SendKeys("12345678");
		var login2Button = driver.FindElement(By.XPath("//button[@class='order-group-element o-button-wrapper o-button-medium o-button-primary' and @data-ti='submit-trigger']"));
		login2Button.Click();

		// ОЖИДАНИЕ ЗАГРУЗКИ
		Thread.Sleep(2000);

		// Проверка приветственного сообщения
		var userNameLink = driver.FindElement(By.XPath("//a[@data-ti='user_name_link' and text()='soweomi@gmail.com']"));
		if (userNameLink.Text.Equals("soweomi@gmail.com"))
		{
			Console.WriteLine("Проверка успешна: имя пользователя отображается корректно.");



			OpenQA.Selenium.Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
			string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "screenshot.png");
			screenshot.SaveAsFile(screenshotPath);

			Console.WriteLine($"Скриншот сохранен как {screenshotPath}");
		}
		else
		{
			Console.WriteLine("Ошибка: имя пользователя не отображается.");
		}
	}
}

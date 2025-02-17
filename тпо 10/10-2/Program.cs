using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

class Program
{
	static void Main()
	{
		var driver = new ChromeDriver();
		try
		{
			driver.Navigate().GoToUrl("https://hotel.tutu.ru/c_belarus/minsk/");
			var loginButton = driver.FindElement(By.XPath("//div[text()='Войти']"));
			loginButton.Click();
			var emailField = driver.FindElement(By.Name("email"));
			emailField.SendKeys("soweomi@gmail.com");
			var passwordField = driver.FindElement(By.Name("password"));
			passwordField.SendKeys("12345678");
			var login2Button = driver.FindElement(By.XPath("//button[@class='order-group-element o-button-wrapper o-button-medium o-button-primary' and @data-ti='submit-trigger']"));
			login2Button.Click();
			System.Threading.Thread.Sleep(2000);
			var cookies = driver.Manage().Cookies.AllCookies;
			using (var file = new StreamWriter(@"C:\Users\yana\OneDrive\Рабочий стол\tpo_10\cookies.json"))
			{
				foreach (var cookie in cookies)
				{
					file.WriteLine($"{cookie.Name}={cookie.Value}");
				}
			}
			driver.Navigate().GoToUrl("https://hotel.tutu.ru/h_otel_ist_taym/");
			var welcomeMessage = driver.FindElement(By.Id("welcome")).Text;
			Console.WriteLine(welcomeMessage);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Произошла ошибка: {ex.Message}");
		}
	}
}
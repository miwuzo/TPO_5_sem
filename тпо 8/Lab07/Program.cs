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

		// АВТОРИЗАЦИЯ
		/*var loginButton = driver.FindElement(By.XPath("//div[text()='Войти']"));
		loginButton.Click();
		var emailField = driver.FindElement(By.Name("email"));
		emailField.SendKeys("soweomi@gmail.com");
		var passwordField = driver.FindElement(By.Name("password"));
		passwordField.SendKeys("12345678");
		var login2Button = driver.FindElement(By.XPath("//button[@class='order-group-element o-button-wrapper o-button-medium o-button-primary' and @data-ti='submit-trigger']"));
		login2Button.Click();
*/
		//ВЫПАДАЮЩИЙ СПИСОК
		/*var buttonElement = driver.FindElement(By.XPath("//div[contains(@class, 'o-button') and contains(@class, 'o-dropdown-elementWrapper')]//button[@data-ti='order-dropdown']"));
		buttonElement.Click();
		Thread.Sleep(1000);
		driver.FindElement(By.XPath("//div[contains(text(), 'Сначала дешёвые')]")).Click();*/

		//ПОИСК
		/*driver.FindElement(By.XPath("//span[text()='Найти']")).Click();
*/
		//СПРАВОЧНАЯ ИНФОРМАЦИЯ
		driver.FindElement(By.XPath("//span[@class='VFkLz33Grg0Jo3fV' and text()='Справочная']")).Click();
		driver.FindElement(By.XPath("//a[text()='Частые вопросы про отели']")).Click();
		var link = driver.FindElement(By.XPath("//a[@href='https://www.tutu.ru/2read/hotels/hotels_faq/#login']"));
		link.Click();
	}
}

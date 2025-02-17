//--------------------1---------------------------

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
	static void Main(string[] args)
	{
		// Создание объекта ChromeOptions
		var options = new ChromeOptions();
		options.AddArgument("--start-maximized"); // Запуск в полноэкранном режиме
		options.AddArgument("--disable-popup-blocking"); // Отключение блокировки всплывающих окон
		options.AddArgument("--incognito"); // Запуск в режиме инкогнито

		// Создание экземпляра ChromeDriver с опциями
		using (IWebDriver driver = new ChromeDriver(options))
		{
			// Переход на тестируемый сайт
			driver.Navigate().GoToUrl("https://hotel.tutu.ru/c_belarus/minsk/");
		}
	}
}



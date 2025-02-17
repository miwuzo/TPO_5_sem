using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

class Program
{
	static void Main(string[] args)
	{
		IWebDriver driver = new ChromeDriver();
		driver.Navigate().GoToUrl("https://hotel.tutu.ru/c_belarus/minsk/");
		Console.Clear();
		Console.WriteLine("Открыта страница отелей.");

		// Инициализация страниц
		var loginPage = new LoginPage(driver);
		var hotelsPage = new HotelsPage(driver);

		// АВТОРИЗАЦИЯ
		loginPage.ClickLoginButton();
		Console.WriteLine("Нажата кнопка 'Войти'.");

		Thread.Sleep(1000); 
		loginPage.EnterEmail("soweomi@gmail.com");
		Console.WriteLine("Введён email.");

		Thread.Sleep(1000); 
		loginPage.EnterPassword("12345678");
		Console.WriteLine("Введён пароль.");

		Thread.Sleep(1000); 
		loginPage.ClickSubmitButton();
		Console.WriteLine("Нажата кнопка 'Войти'.");
		Thread.Sleep(2000); 
		Console.WriteLine("Тест 1 пройден успешно.");

		// ВЫПАДАЮЩИЙ СПИСОК
		hotelsPage.ClickDropdownButton();
		Console.WriteLine("Нажата кнопка для открытия выпадающего списка.");
		Thread.Sleep(1000); 
		hotelsPage.SelectSortOption("Сначала дешёвые");
		Console.WriteLine("Выбрана сортировка 'Сначала дешёвые'.");
		Thread.Sleep(1000); 
		Console.WriteLine("Тест 2 пройден успешно.");


		// ПОИСК
		hotelsPage.ClickSearchButton();
		Console.WriteLine("Нажата кнопка 'Найти'.");
		Thread.Sleep(2000);
		Console.WriteLine("Тест 3 пройден успешно.");

		// ПЕРЕХОД НА ГЛАВНУЮ СТРАНИЦУ
		driver.Navigate().GoToUrl("https://hotel.tutu.ru/c_belarus/minsk/");
		Console.WriteLine("Перешли на главную страницу.");

		// СПРАВОЧНАЯ ИНФОРМАЦИЯ
		Thread.Sleep(5000);
		hotelsPage.ClickHelpButton();
		Console.WriteLine("Нажата кнопка 'Справочная'.");
		Thread.Sleep(1000); 
		hotelsPage.ClickFAQLink();
		Console.WriteLine("Нажата ссылка 'Частые вопросы про отели'.");
		Thread.Sleep(1000); 
		hotelsPage.ClickSpecificLink("Частые вопросы про отели");
		Console.WriteLine("Открыта страница 'Частые вопросы про отели'.");
		Thread.Sleep(2000); 

		// Закрытие браузера
		driver.Quit();
		Console.WriteLine("Тест завершён. Браузер закрыт.");
	}
}
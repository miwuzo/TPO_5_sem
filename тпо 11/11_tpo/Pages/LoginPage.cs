using OpenQA.Selenium;

public class LoginPage
{
	private IWebDriver driver;

	public LoginPage(IWebDriver driver)
	{
		this.driver = driver;
	}

	public void ClickLoginButton()
	{
		driver.FindElement(By.XPath("//div[text()='Войти']")).Click();
	}

	public void EnterEmail(string email)
	{
		var emailField = driver.FindElement(By.Name("email"));
		emailField.SendKeys(email);
	}

	public void EnterPassword(string password)
	{
		var passwordField = driver.FindElement(By.Name("password"));
		passwordField.SendKeys(password);
	}

	public void ClickSubmitButton()
	{
		var loginButton = driver.FindElement(By.XPath("//button[@class='order-group-element o-button-wrapper o-button-medium o-button-primary' and @data-ti='submit-trigger']"));
		loginButton.Click();
	}
}
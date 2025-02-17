/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Lab09;
using NUnit.Framework;

namespace OrderProcessing.Tests
{

	// -----------------------------10 лр - 5-----------------------------
	[TestFixture]
	public class OrderServiceTests
	{
		private Mock<IPaymentService> _mockPaymentService; //заглушка (объект для имитации поведения реального объекта)
		private OrderService _orderService; //то, что тестируем

		[SetUp] //настройки перед тестом
		public void Setup()
		{
			_mockPaymentService = new Mock<IPaymentService>(); //инициализ заглушку
			_orderService = new OrderService(_mockPaymentService.Object); //создаем объект с заглушкой
		}

		[TearDown] // Этот метод выполняется после каждого теста
		public void Cleanup()
		{
			_mockPaymentService = null;
			_orderService = null;
		}


		// --------------------------------------------------------------


		[Test]
		public void PlaceOrder_ValidOrder_ReturnsTrue()
		{
			// подготовка данных для теста
			var order = new Order { Amount = 100 };
			_mockPaymentService.Setup(ps => ps.ProcessPayment(order.Amount)).Returns(true);

			// выполнение тестируемого метода
			var result = _orderService.PlaceOrder(order);

			// проверка результатов
			Assert.IsTrue(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(order.Amount), Times.Once);
		}

		[Test]
		public void PlaceOrder_ZeroAmount_ReturnsFalse()
		{
			// Arrange
			var order = new Order { Amount = 0 };

			// Act
			var result = _orderService.PlaceOrder(order);

			// Assert
			Assert.IsFalse(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(It.IsAny<decimal>()), Times.Never);
		}
	}

}



*/











//------------------------------------6-----------------------

/*


using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OrderProcessing.Tests
{
	[TestFixture]
	public class OrderServiceTests
	{
		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
		}

		[Test]
		public void TestPageTitle()
		{
			driver.Navigate().GoToUrl("https://hotel.tutu.ru/c_belarus/minsk/");
			Assert.IsTrue(driver.Title.Contains("Минск"), "Заголовок страницы не содержит 'Минск'");
		}

		[TearDown]
		public void Cleanup()
		{
			// Освобождение ресурсов
			if (driver != null)
			{
				driver.Quit();
				driver.Dispose(); // Освобождение ресурсов
			}
		}
	}
}
*/



















//---------------------------------7----------------------------------
//cd Y:\_V\тпо\тпо 9\OrderProcessing.Tests
//Y:
//dotnet test --filter "Category=Positive"

using System;
using Moq;
using NUnit.Framework;
using Lab09;

namespace OrderProcessing.Tests
{
	[TestFixture]
	public class OrderServiceTests
	{
		private Mock<IPaymentService> _mockPaymentService;
		private OrderService _orderService;

		[SetUp]
		public void Setup()
		{
			_mockPaymentService = new Mock<IPaymentService>();
			_orderService = new OrderService(_mockPaymentService.Object);
		}

		[TearDown]
		public void Cleanup()
		{
			_mockPaymentService = null;
			_orderService = null;
		}

		// Тест, который должен пройти
		[Test]
		[Category("Positive")]
		public void PlaceOrder_ValidOrder_ReturnsTrue()
		{
			var order = new Order { Amount = 100 };
			_mockPaymentService.Setup(ps => ps.ProcessPayment(order.Amount)).Returns(true);

			var result = _orderService.PlaceOrder(order);

			Assert.IsTrue(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(order.Amount), Times.Once);
		}

		// Тест, который ожидаемо падает
		[Test]
		[Category("Negative")]
		[Ignore("Тест временно игнорируется")]
		public void PlaceOrder_ZeroAmount_ReturnsFalse()
		{
			var order = new Order { Amount = 0 };

			var result = _orderService.PlaceOrder(order);

			Assert.IsFalse(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(It.IsAny<decimal>()), Times.Never);
		}

		// Тест, который ожидаемо падает
		[Test]
		[Category("Negative")]
		[Order(1)]
		public void PlaceOrder_NegativeAmount_ReturnsFalse()
		{
			var order = new Order { Amount = -50 };

			var result = _orderService.PlaceOrder(order);

			Assert.IsFalse(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(It.IsAny<decimal>()), Times.Never);
		}
	}
}


































/*
//--8

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Moq;
using NUnit.Framework;

namespace OrderProcessing.Tests
{
	public interface IPaymentService
	{
		bool ProcessPayment(decimal amount);
	}

	public class Order
	{
		public decimal Amount { get; set; }
	}

	public class OrderService
	{
		private readonly IPaymentService _paymentService;

		public OrderService(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		public bool PlaceOrder(Order order)
		{
			if (order.Amount <= 0) return false;
			return _paymentService.ProcessPayment(order.Amount);
		}
	}

	[TestFixture]
	public class OrderServiceTests
	{
		private Mock<IPaymentService> _mockPaymentService;
		private OrderService _orderService;

		[SetUp]
		public void Setup()
		{
			_mockPaymentService = new Mock<IPaymentService>();
			_orderService = new OrderService(_mockPaymentService.Object);
		}

		[TearDown]
		public void Cleanup()
		{
			_mockPaymentService = null;
			_orderService = null;
		}

		// Тест, который должен пройти
		[Test]
		[Category("Positive")]
		public void PlaceOrder_ValidOrder_ReturnsTrue()
		{
			var order = new Order { Amount = 100 };
			_mockPaymentService.Setup(ps => ps.ProcessPayment(order.Amount)).Returns(true);

			var result = _orderService.PlaceOrder(order);

			Assert.IsTrue(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(order.Amount), Times.Once);
		}

		// Тест, который ожидаемо падает
		[Test]
		[Category("Negative")]
		[Ignore("Тест временно игнорируется")]
		public void PlaceOrder_ZeroAmount_ReturnsFalse()
		{
			var order = new Order { Amount = 0 };

			var result = _orderService.PlaceOrder(order);

			Assert.IsFalse(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(It.IsAny<decimal>()), Times.Never);
		}

		// Тест, который ожидаемо падает
		[Test]
		[Category("Negative")]
		public void PlaceOrder_NegativeAmount_ReturnsFalse()
		{
			var order = new Order { Amount = -50 };

			var result = _orderService.PlaceOrder(order);

			Assert.IsFalse(result);
			_mockPaymentService.Verify(ps => ps.ProcessPayment(It.IsAny<decimal>()), Times.Never);
		}
	}

	[TestFixture]
	public class TestResultLogger
	{
		public const string LogFilePath = "Y:\\_V\\тпо\\тпо 9\\OrderProcessing.Tests\\TestResults.json";

		[OneTimeTearDown]
		public void LogResults()
		{
			var testResults = new List<TestResult>();

			var result = TestContext.CurrentContext.Result;

			var testResult = new TestResult
			{
				Name = TestContext.CurrentContext.Test.Name,
				Outcome = result.Outcome.Status.ToString()
			};

			testResults.Add(testResult);

			File.WriteAllText(LogFilePath, JsonConvert.SerializeObject(testResults, Formatting.Indented));
		}

		public class TestResult
		{
			public string Name { get; set; }
			public string Outcome { get; set; }
			public double Duration { get; set; }
		}
	}
}*/
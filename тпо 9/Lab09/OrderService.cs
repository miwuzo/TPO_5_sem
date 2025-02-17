using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab09
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
		}
	}
	public class OrderService // логика обработки заказов
	{
		private readonly IPaymentService _paymentService;

		public OrderService(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		public bool PlaceOrder(Order order) //проверка является ли сумма заказа действительной
		{
			if (order.Amount <= 0)
				return false;

			return _paymentService.ProcessPayment(order.Amount);
		}
	}

	public interface IPaymentService //успешно или нет обработан заказ
	{
		bool ProcessPayment(decimal amount);
	}

	public class Order //модель заказа
	{
		public decimal Amount { get; set; } //сумма заказа
	}
}

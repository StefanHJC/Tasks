namespace IMJunior
{
    public class Program
    {
        static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();
            var paymentSystemFactory = new PaymentSystemFactory();

            var systemId = orderForm.ShowForm();

            IPaymentSystem paymentSystem = paymentSystemFactory.CreatePaymentSystem(systemId);
            paymentHandler.ShowPaymentResult(paymentSystem);
        }
    }

    public interface IPaymentSystem
    {
        public void ExecutePayment();

        public bool IsPaymentExecuted();
    }

    public class PaymentSystemFactory
    {
        private IPaymentSystem _paymentSystem;

        public IPaymentSystem CreatePaymentSystem(string paymentSystem)
        {
            switch (paymentSystem)
            {
                case "QIWI":
                    _paymentSystem = new QIWI();
                    break;

                case "Webmoney":
                    _paymentSystem = new Webmoney();
                    break;

                case "Card":
                    _paymentSystem = new Card();
                    break;

                case "YobaPay":
                    _paymentSystem = new YobaPay();
                    break;
                default:
                    throw new ArgumentException();
            }
            return _paymentSystem;
        }
    }

    public class QIWI : IPaymentSystem
    {
        public void ExecutePayment()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }

        public bool IsPaymentExecuted()
        {
            return true;
        }
    }

    public class Webmoney : IPaymentSystem
    {
        public void ExecutePayment()
        {
            Console.WriteLine("Вызов API WebMoney...");
        }

        public bool IsPaymentExecuted()
        {
            return true;
        }
    }

    public class Card : IPaymentSystem
    {
        public void ExecutePayment()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }

        public bool IsPaymentExecuted()
        {
            return true;
        }
    }

    public class YobaPay : IPaymentSystem
    {
        public void ExecutePayment()
        {
            Console.WriteLine("ALLO YOBA ETO TI?");
        }

        public bool IsPaymentExecuted()
        {
            return true;
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card, YobaPay");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(IPaymentSystem paymentSystem)
        {
            Console.WriteLine($"Вы оплатили с помощью {paymentSystem}");

            paymentSystem.ExecutePayment();

            if (paymentSystem.IsPaymentExecuted())
                Console.WriteLine("Оплата прошла успешно!");
        }
    }
}
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var paymentSystem1 = new FirstPaymentSystem();
        var paymentSystem2 = new SecondPaymentSystem();
        var paymentSystem3 = new ThirdPaymentSystem();
        var order = new Order(1, 12000);

        Console.WriteLine(paymentSystem1.GetPayingLink(order));
        Console.WriteLine(paymentSystem2.GetPayingLink(order));
        Console.WriteLine(paymentSystem3.GetPayingLink(order));
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

public interface IPaymentSystem
{
    public string GetPayingLink(Order order);
}

public class FirstPaymentSystem : IPaymentSystem
{
    public virtual string GetPayingLink(Order order)
    {
        string hash = ComputeMD5(order.Amount.ToString());

        return $"pay.system1.ru/order?amount={order.Amount}RUB&hash={hash}";
    }

    protected string ComputeMD5(string value)
    {
        var md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
        string hash = Convert.ToBase64String(hashBytes);

        return hash;
    }
}

public class SecondPaymentSystem : FirstPaymentSystem
{
    public override string GetPayingLink(Order order)
    {
        string hash = ComputeMD5((order.Id + order.Amount).ToString());

        return $"order.system2.ru/pay?hash={hash}";
    }
}

public class ThirdPaymentSystem : IPaymentSystem
{
    public string GetPayingLink(Order order)
    {
        var sha1 = SHA1.Create();
        string secretKey = GetSecretKey();

        byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes((order.Amount + order.Id).ToString() + secretKey));
        string hash = Convert.ToBase64String(hashBytes);

        return $"system3.com/pay?amount={order.Amount}&curency=RUB&hash={hash}";
    }

    private string GetSecretKey()
    {
        return Console.ReadLine();
    }
}

public class Good
{
    private string _name;

    public Good(string name)
    {
        _name = name;
    }
}

public class Warehouse
{
    private Dictionary<Good, uint> _goods;

    public IReadOnlyDictionary<Good, uint> Goods => _goods;

    public Warehouse()
    {
        _goods = new Dictionary<Good, uint>();
    }

    public void Delive(Good good, int amount)
    {
        if (amount <= 0)
            throw new Exception();

        _goods.Add(good, (uint)amount);
    }

    public void PrintGoods()
    {
        foreach (var good in _goods)
            Console.WriteLine($"{good.Key} остаток {good.Value}");
    }

    public void RemoveOrdered(IReadOnlyDictionary<Good, uint> orderedGoods)
    {
        foreach (var good in orderedGoods)
            _goods[good.Key] -= good.Value;
    }
}

public class Shop
{
    private Warehouse _warehouse;

    public Warehouse Warehouse => _warehouse;

    public Shop(Warehouse warehouse)
    {
        _warehouse = warehouse;
    }

    public Cart Cart()
    {
        return new Cart(this);
    }
}

public class Cart
{
    private Dictionary<Good, uint> _goodsInCart;
    private Shop _shop;

    public Cart(Shop shop)
    {
        _goodsInCart = new Dictionary<Good, uint>();
        _shop = shop;
    }

    public void Add(Good good, int amount)
    {
        if (amount <= 0)
            throw new Exception();
        else if ((int)_shop.Warehouse.Goods[good] - amount < 0)
            throw new Exception($"Остаток на складе - {_shop.Warehouse.Goods[good]}");

        _goodsInCart.Add(good, (uint)amount);
    }

    public void PrintGoods()
    {
        foreach (var good in _goodsInCart)
            Console.WriteLine($"{good.Key} - {good.Value}");
    }

    public Order Order()
    {
        return new Order(_goodsInCart, _shop);
    }
}

public class Order
{
    private Dictionary<Good, uint> _orderedGoods;

    public IReadOnlyDictionary<Good, uint> OrderedGoods => _orderedGoods;
    public string Paylink { get; private set; }

    public Order(Dictionary<Good, uint> orderedGoods, Shop shop)
    {
        _orderedGoods = orderedGoods;
        shop.Warehouse.RemoveOrdered(OrderedGoods);

        Paylink = GeneratePaylink();
    }

    private string GeneratePaylink()
    {
        return "гыыыыыы";
    }
}
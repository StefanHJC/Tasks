public class Weapon
{
    private uint _damage;
    private uint _bullets;

    public Weapon(int damage, int bullets)
    {
        if (damage < 0 || bullets < 0)
            throw new Exception();

        _damage = (uint)damage;
        _bullets = (uint)bullets;
    }

    public void Fire(Player player)
    {
        if (_bullets != 0)
        {
            player.TakeDamage(_damage);
            _bullets -= 1;
        }
    }
}

public class Player
{
    private uint _health;

    public Player(int health)
    {
        if (health <= 0)
            throw new Exception();

        _health = (uint)health;
    }

    public void TakeDamage(uint damage)
    {
        _health -= damage;
    }
}

public class Bot
{
    private Weapon _weapon;

    public Bot(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}
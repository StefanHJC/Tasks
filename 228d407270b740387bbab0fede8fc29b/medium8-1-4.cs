class Player
{
    private Weapon _weapon;

    public Vector2 MovementDirection { get; private set; }
    public float MovementSpeed { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
    public bool IsReloading => _weapon.Bullets == 0;

    public Player(Weapon weapon, string name, float movementSpeed, int age)
    {
        _weapon = weapon;
        Name = name;
        MovementSpeed = movementSpeed;
        Age = age;
    }

    public void Move()
    {
        //Do move
    }

    public void Attack()
    {
        //attack
    }
}

class Weapon
{
    public float Damage { get; private set; }
    public float ReloadingTime { get; private set; }
    public int Bullets { get; private set; }
    public int MaxBullets { get; private set; }

    public Weapon(float damage, float reloadingTime, int maxBullets)
    {
        Damage = damage;
        ReloadingTime = reloadingTime;
        MaxBullets = maxBullets;
        Bullets = maxBullets;
    }

    public void Shoot()
    {
        // make shot
    }
}

struct Vector2
{
    public float X;
    public float Y;

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }
}
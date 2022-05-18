class Weapon
{
    private int _bullets;
    private readonly int _maxBullets;
    private readonly int _minBullets;
    private readonly int _bulletsPerShot;

    public Weapon(int maxBullets, int bulletsPerShot)
    {
        _maxBullets = maxBullets;
        _bulletsPerShot = bulletsPerShot;
        _minBullets = bulletsPerShot;
        _bullets = maxBullets;
    }

    public bool CanShoot() => _bullets > _minBullets;

    public void Shoot() => _bullets -= _bulletsPerShot;
}
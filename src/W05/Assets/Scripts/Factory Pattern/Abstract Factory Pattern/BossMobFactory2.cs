public class BossMobFactory2 : BossMobFactory
{
    public override Monster CreateMonster()
    {
        Monster monster = new SphereMonster();
        return monster;
    }

    public override Weapon CreateWeapon()
    {
        Weapon weapon = new BulletWeapon();
        return weapon;
    }
}
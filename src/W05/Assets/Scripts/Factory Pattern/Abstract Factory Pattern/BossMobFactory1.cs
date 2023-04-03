public class BossMobFactory1 : BossMobFactory
{
    public override Monster CreateMonster()
    {
        Monster monster = new CubeMonster();
        return monster;
    }

    public override Weapon CreateWeapon()
    {
        Weapon weapon = new MissileWeapon();
        return weapon;
    }
}
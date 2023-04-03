using UnityEngine;

public abstract class BossMobFactory : MonoBehaviour
{
    public BossMob CreateBoss()
    {
        BossMob boss = new BossMob
        {
            monster = CreateMonster(),
            weapon = CreateWeapon()
        };

        return boss;
    }

    public abstract Monster CreateMonster();
    public abstract Weapon CreateWeapon();
}
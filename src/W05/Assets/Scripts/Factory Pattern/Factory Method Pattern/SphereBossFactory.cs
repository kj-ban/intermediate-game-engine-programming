public class SphereBossFactory : BossFactory
{
    public GreenSphereBoss greenSphereBoss;
    public RedSphereBoss redSphereBoss;
    public override BossBase CreateBoss(string type)
    {
        BossBase boss = null;

        if (type.Equals("green"))
        {
            boss = Instantiate(greenSphereBoss);
        }
        else if (type.Equals("red"))
        {
            boss = Instantiate(redSphereBoss);
        }

        return boss;
    }
}
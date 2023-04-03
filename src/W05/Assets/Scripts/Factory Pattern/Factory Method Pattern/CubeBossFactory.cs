public class CubeBossFactory : BossFactory
{
    public GreenCubeBoss greenCubeBoss;
    public RedCubeBoss redCubeBoss;

    public override BossBase CreateBoss(string type)
    {
        BossBase boss = null;
        if (type.Equals("green")) 
        {
            boss = Instantiate(greenCubeBoss);
        }
        else if (type.Equals("red"))
        {
            boss = Instantiate(redCubeBoss);
        }

        return boss;
    }
}
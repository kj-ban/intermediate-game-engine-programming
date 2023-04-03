using UnityEngine;

public class BattleGenerator : MonoBehaviour
{
    void Start()
    {
        BossMobFactory factory1 = new BossMobFactory1();
        BossMob boss1 = factory1.CreateBoss();
    }
}
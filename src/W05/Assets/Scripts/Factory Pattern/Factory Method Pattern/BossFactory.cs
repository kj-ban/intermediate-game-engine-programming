using UnityEngine;

public abstract class BossFactory : MonoBehaviour
{
    public abstract BossBase CreateBoss(string type);
}


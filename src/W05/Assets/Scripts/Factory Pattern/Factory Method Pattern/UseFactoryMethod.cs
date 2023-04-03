using UnityEngine;

public class UseFactoryMethod : MonoBehaviour
{
    public void EventTime()
    {
        BossFactory bf = GetComponent<CubeBossFactory>();
        BossBase boss = bf.CreateBoss("green");
        boss.transform.position = transform.position;
        boss.transform.LookAt(Vector3.zero);
    }
}
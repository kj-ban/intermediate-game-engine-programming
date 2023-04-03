using UnityEngine;

public class SelfDestructable : MonoBehaviour
{
    public float lifeTime;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
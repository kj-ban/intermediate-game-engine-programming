using UnityEngine;

public class SphereSpawner : SpawnerBase
{
    public override void Spawn()
    {
        Debug.Log("SphereSpawner.Spawn");
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }
}
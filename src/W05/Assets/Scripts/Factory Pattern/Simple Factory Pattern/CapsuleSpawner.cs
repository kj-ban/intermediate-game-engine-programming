using UnityEngine;

public class CapsuleSpawner : SpawnerBase
{
    public override void Spawn()
    {
        Debug.Log("CapsuleSpawner.Spawn");
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
    }
}
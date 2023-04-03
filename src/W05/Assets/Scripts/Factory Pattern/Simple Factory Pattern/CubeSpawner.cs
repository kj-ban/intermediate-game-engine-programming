using UnityEngine;

public class CubeSpawner : SpawnerBase
{
    public override void Spawn()
    {
        Debug.Log("CubeSpawner.Spawn");
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
}
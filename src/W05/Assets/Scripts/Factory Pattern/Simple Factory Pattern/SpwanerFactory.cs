using UnityEngine;

public enum SpawnerType
{
    cube,
    capsule,
    sphere
}

public class SpwanerFactory : MonoBehaviour
{
    public static SpawnerBase createSpwaner(SpawnerType type)
    {
        GameObject obj = null;
        SpawnerBase spawner = null;
        switch (type)
        {
            case SpawnerType.cube:
                obj = Instantiate(Resources.Load<GameObject>("CubeSpawner"));
                spawner = obj.GetComponent<CubeSpawner>();
                break;

            case SpawnerType.capsule:
                obj = Instantiate(Resources.Load<GameObject>("CapsuleSpawner"));
                spawner = obj.GetComponent<CapsuleSpawner>();
                break;

            case SpawnerType.sphere:
                obj = Instantiate(Resources.Load<GameObject>("SphereSpawner"));
                spawner = obj.GetComponent<SphereSpawner>();
                break;
        }
        return spawner;
    }
}
using UnityEngine;

public class Television
{
    public void Start()
    {
        Debug.Log("Turn On TV");
    }
}

public class Remote
{
    private Television TV { get; set; }

    private static Remote _remoteController;

    protected Remote()
    {
        TV = new Television();
    }

    static Remote()
    {
        _remoteController = new Remote();
    }

    public static Remote Control
    {
        get { return _remoteController; }
    }

    public void RunTV()
    {
        TV.Start();
    }
}

public class TightCouplingSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Remote.Control.RunTV();
    }
}

using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class TriggerEvent : UnityEvent<Collider>
{
}


public class Triggerable : MonoBehaviour
{
    private Rigidbody rb;
    public TriggerEvent onTriggerEnter;

    private void Awake()
    {
        rb = gameObject.GetOrAddComponent<Rigidbody>();
        gameObject.GetOrAddComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }

    private void OnDestroy()
    {
        onTriggerEnter.RemoveAllListeners();
    }
}
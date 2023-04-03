using ExtensionMethods;
//using Unity.VisualScripting;
using UnityEngine;

public class Examples : MonoBehaviour
{
    void Start()
    {
        string s = "Hello Extension Methods";
        Debug.Log(s.WordCount());

        int i = MyExtensions.WordCount(s);
        Debug.Log(i);

        AudioSource audioSource = gameObject.GetOrAddComponent<AudioSource>();
        audioSource = Util.GetOrAddComponent<AudioSource>(gameObject);
        //audioSource = transform.GetOrAddComponent<AudioSource>();
    }
}
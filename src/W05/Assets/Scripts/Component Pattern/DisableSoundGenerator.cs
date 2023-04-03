using UnityEngine;

public class DisableSoundGenerator : MonoBehaviour
{
    public AudioClip sound;
 
    private void OnDisable()
    {
        AudioManager.Instance.PlaySound(sound);
    }
}
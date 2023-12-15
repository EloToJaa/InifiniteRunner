using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private bool muted;
    private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleMuted()
    {
        muted = !muted;

        audioSource.mute = muted;
    }

    public bool GetMuted()
    {
        return muted;
    }
}

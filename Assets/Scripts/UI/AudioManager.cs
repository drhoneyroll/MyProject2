using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip punch;
    public AudioClip death;
    public AudioClip game_over;
    public AudioClip block;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX (AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

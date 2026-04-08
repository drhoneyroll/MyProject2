using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip punch;
    public AudioClip death;
    public AudioClip game_over;
    public AudioClip roll_block;
    [SerializeField] public AudioClip health_pickup;
    [SerializeField] public AudioClip roll_attack;
    [SerializeField] public AudioClip roll_hit;
    [SerializeField] public AudioClip game_over_death;
    [SerializeField] public AudioClip constant_block;
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

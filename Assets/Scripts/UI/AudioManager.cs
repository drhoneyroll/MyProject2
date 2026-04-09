using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    [SerializeField] AudioSource SFXSource;

    [SerializeField] public AudioSource LoopingSFX;

    public AudioClip background;
    public AudioClip punch;
    public AudioClip death;
    //public AudioClip game_over;
    public AudioClip roll_block;
    [SerializeField] public AudioClip health_pickup;
    [SerializeField] public AudioClip roll_attack;
    [SerializeField] public AudioClip roll_hit;
    [SerializeField] public AudioClip game_over_death;
    [SerializeField] public AudioClip constant_block;

    [SerializeField] public float background_volume;
    [SerializeField] public float punch_volume;
    [SerializeField] public float death_volume;
    //[SerializeField] public float game_over_volume;
    [SerializeField] public float roll_block_volume;
    [SerializeField] public float health_pickup_volume;
    [SerializeField] public float roll_attack_volume;
    [SerializeField] public float roll_hit_volume;
    [SerializeField] public float game_over_death_volume;
    [SerializeField] public float constant_block_volume;

    private float volume;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.volume = background_volume;
        LoopingSFX.volume = constant_block_volume;
        musicSource.Play();
    }

    public void LoopSFX ()
    {
        LoopingSFX.clip = constant_block;
        LoopingSFX.Play();
    }
    public void StopSFX ()
    {
        LoopingSFX.clip = constant_block;
        LoopingSFX.Stop();
    }
    public void PlaySFX (AudioClip clip)
    {
        //Use Switch Statement
        if (clip == punch)
        {
            volume = punch_volume;
        }
        else if (clip == death)
        {
            volume = death_volume;
        } 
        /*else if (clip == game_over)
        {
            volume = game_over_volume;
        }*/
        else if (clip == roll_block)
        {
            volume = roll_block_volume;
        }
        else if (clip == health_pickup)
        {
            volume = health_pickup_volume;
        }
        else if (clip == roll_attack)
        {
            volume = roll_attack_volume;
        }
        else if (clip == roll_hit)
        {
            volume = roll_hit_volume;
        }
        else if (clip == game_over_death)
        {
            volume = game_over_death_volume;
        }
        else if (clip == constant_block)
        {
            volume = constant_block_volume;
        }
        SFXSource.PlayOneShot(clip, volume);
    }
}

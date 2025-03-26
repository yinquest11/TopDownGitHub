using UnityEngine;

public class Audioo : MonoBehaviour
{
    //A script for Picked up sound's empty GameObject

    public AudioClip clip;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();

        if ( clip == null) { Debug.LogWarning(gameObject.name + ": ( clip ) is missing."); return; }

        source.clip = clip;
        source.PlayOneShot(clip);
    }

    
   
}

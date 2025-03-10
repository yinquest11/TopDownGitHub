using UnityEngine;

public class Audioo : MonoBehaviour
{

    public AudioClip clip;
    private AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();

        if ( clip == null) { Debug.LogWarning(gameObject.name + ": ( clip ) is missing."); return; }
        source.clip = clip;
        source.PlayOneShot(clip);
    }

    
   
}

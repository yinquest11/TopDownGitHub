using UnityEngine;

public class MakeSound : MonoBehaviour
{
    public AudioClip Clip;
    private AudioSource Source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //paly sound
        Source = GetComponent<AudioSource>();
        Source.PlayOneShot(Clip,7.0f);

        //call the destroy functuin afetr 1 second.
        Invoke("Destroyy", 1f);
        
    }

    //function to destroy the object
    private void Destroyy()
    {
        Destroy(gameObject);    
    }

}

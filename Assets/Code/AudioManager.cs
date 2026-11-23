using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

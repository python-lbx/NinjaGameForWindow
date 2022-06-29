using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] //具象化
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float ptich;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

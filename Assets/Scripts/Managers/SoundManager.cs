using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance = null;

    private AudioSource audioSource;
    private Dictionary<string, AudioClip> clipMap = new Dictionary<string, AudioClip>();

    /// <summary>
    /// Awake Soundmanager
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _init();
        DontDestroyOnLoad(gameObject);

    }

    /// <summary>
    /// Init the damn thing
    /// </summary>
    private void _init()
    {
        audioSource = GetComponent<AudioSource>();
        _loadSoundFromResources();
    }

    /// <summary>
    /// Loads sounds from resources/sounds folder
    /// </summary>
    private void _loadSoundFromResources()
    {
        UnityEngine.Object[] sounds = Resources.LoadAll("sounds", typeof(AudioClip)).Cast<AudioClip>().ToArray(); ;
        foreach (AudioClip t in sounds)
        {
            Debug.Log("Auto-loading sound " + t.name);
            clipMap.Add(t.name, t);
        }
    }

    /// <summary>
    /// Play sound by name
    /// </summary>
    /// <param name="name"></param>
    public void PlaySoundByName(String name)
    {
        AudioClip clip;
        if (clipMap.TryGetValue(name, out clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Sound '" + name + "' was not found!");
        }
    }
}

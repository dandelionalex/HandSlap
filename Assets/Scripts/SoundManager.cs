using System;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    [SerializeField] 
    private SoundConfig[] sounds;

    [SerializeField] 
    private AudioSource audioSource;
    
    private static SoundManager inventory;
    
    private void Awake()
    {
        if (inventory != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this);
        inventory = this;
    }
    
    public void PlaySound(SoundType sound)
    {
        var soundConfig = sounds.FirstOrDefault(s => s.soundType == sound);
        if(soundConfig == null)
            return;
        
        audioSource.PlayOneShot(soundConfig.clip);
    }

    [Serializable]
    public class SoundConfig
    {
        public SoundType soundType;
        public AudioClip clip;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return _instance;
        }
    }

    private void Awake()
    {
        SoundManager.Initialize();
    }

    public void Start()
    {
        _instance = this;
    }


    public soundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class soundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        public float volume;
    }



}

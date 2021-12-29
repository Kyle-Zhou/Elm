using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{

    public enum Sound
    {
        playerAttack,
        playerDash,
        EnemyHit,
        PlayerMove,
        TreeHit,
        RockHit,
        EnemyDeath,
        ArrowWhoosh,
        ItemPickup,
        CollectWheatAndCarrots,
        LevelUp,
        CollectBlueberries,
        playerDamage,
        playerDeath,
        pop,
        click,
        bubbling,
        potLidOpen,
        potLipClose,
        popToClick,
        clickToPop,
        deleteSwoosh,
        crunchBite,
        hammerDrop,
        thud,

        exploreTrack,
        homeTrack,
        bossTrack,

        anvil,
        chestOpen,
        backpackOpen,
        forgeOpen,
        skillUnlocked,

        blightedPineDamage,
        pineconeShatter,
        needleShot,
        rootRise,

        heavyAttack,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    private static GameObject oneShotGameObject;
    private static GameObject loopedSoundGameObject;
    private static GameObject soundTrackGameObject1, soundTrackGameObject2, soundTrackGameObject3;
    private static AudioSource oneShotAudioSource;
    private static AudioSource soundTrackAudioSource, soundTrackAudioSource2, soundTrackAudioSource3;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0;
    }

    //public static void PlaySound(Sound sound, Vector3 position)
    //{
    //    if (CanPlaySound(sound))
    //    {
    //        GameObject soundGameObject = new GameObject("Sound");
    //        soundGameObject.transform.position = position;
    //        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
    //        audioSource.clip = GetAudioClip(sound);
    //        audioSource.Play();

            //Destroy game object 

    //    }
    //}


    public static void PlaySound(Sound sound) 
    {
        if (CanPlaySound(sound)) {
            if (oneShotGameObject == null) { 
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
           
            oneShotAudioSource.volume = GetVolumeLevel(sound);
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }


    public static AudioSource PlayLoopedSound(Sound sound)
    {
        if (loopedSoundGameObject == null)
        {
            loopedSoundGameObject = new GameObject("Looped Sound");
            oneShotAudioSource = loopedSoundGameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.clip = GetAudioClip(sound);
        oneShotAudioSource.loop = true;

        oneShotAudioSource.volume = GetVolumeLevel(sound);

        oneShotAudioSource.Play();
        return oneShotAudioSource;
    }

    public static AudioSource PlaySoundTrack(Sound sound) 
    {
        if (soundTrackGameObject1 == null)
        {
            soundTrackGameObject1 = new GameObject("soundtrack1");
            soundTrackAudioSource = soundTrackGameObject1.AddComponent<AudioSource>();
        }
        soundTrackAudioSource.clip = GetAudioClip(sound);
        soundTrackAudioSource.loop = true;

        soundTrackAudioSource.volume = 0f;
        soundTrackAudioSource.Play();
        AudioFadeOut.instance.StartFadeIn(soundTrackAudioSource, 2.0f, 0.15f);
        return soundTrackAudioSource;
    }

    public static AudioSource PlaySoundTrack2(Sound sound)
    {
        if (soundTrackGameObject2 == null)
        {
            soundTrackGameObject2 = new GameObject("soundtrack2");
            soundTrackAudioSource2 = soundTrackGameObject2.AddComponent<AudioSource>();
        }
        soundTrackAudioSource2.clip = GetAudioClip(sound);
        soundTrackAudioSource2.loop = true;

        soundTrackAudioSource2.volume = 0f;
        soundTrackAudioSource2.Play();
        AudioFadeOut.instance.StartFadeIn(soundTrackAudioSource2, 2.0f, 0.15f);
        return soundTrackAudioSource2;
    }

    public static AudioSource PlaySoundTrack3(Sound sound)
    {
        if (soundTrackGameObject3 == null)
        {
            soundTrackGameObject3 = new GameObject("soundtrack2");
            soundTrackAudioSource3 = soundTrackGameObject3.AddComponent<AudioSource>();
        }
        soundTrackAudioSource3.clip = GetAudioClip(sound);
        soundTrackAudioSource3.loop = true;

        soundTrackAudioSource3.volume = 0f;
        soundTrackAudioSource3.Play();
        AudioFadeOut.instance.StartFadeIn(soundTrackAudioSource3, 2.0f, 0.15f);
        return soundTrackAudioSource3;
    }

    public static void StopSoundtrack(AudioSource audioSource)
    {

        audioSource.volume = 0.2f;
        AudioFadeOut.instance.StartFadeOut(audioSource, 2.0f, 0);
        //audioSource.Stop();
        //audioSource.volume = startVolume;
    }



    public static void StopLoopedSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.33f;
                    if (EnergyBar.instance.GetEnergy() <= EnergyBar.instance.GetEnergyTiredBound()) {
                        playerMoveTimerMax = 0.6f;
                    }
                    
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return true;
                }
                //break;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.soundAudioClip soundAudioClip in GameAssets.instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found");
        return null;
    }

    private static float GetVolumeLevel(Sound sound)
    {
        foreach (GameAssets.soundAudioClip soundAudioClip in GameAssets.instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound != 0)
            {
                return soundAudioClip.volume;
            }
        }
        Debug.LogError("Volume from " + sound + " not found: default to 1.0f");
        return 1.0f;
    }



}

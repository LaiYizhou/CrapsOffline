using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    public static AudioControl Instance;

    [SerializeField]
    private List<AudioClip> audioClipList;

    [Space(10)]
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource soundAudioSource;

    private bool isMusicOn;
    public bool IsMusicOn
    {
        get
        {
            if (!PlayerPrefs.HasKey("IsMusicOn"))
                return true;
            else
            {
                return PlayerPrefs.GetInt("IsMusicOn") == 1;
            }
        }
        set
        {
            isMusicOn = value;
            PlayerPrefs.SetInt("IsMusicOn", isMusicOn ? 1 : 0);
            UpdateMusicState();
        }
    }

    private bool isSoundOn;
    public bool IsSoundOn
    {
        get
        {
            if (!PlayerPrefs.HasKey("IsSoundOn"))
                return true;
            else
            {
                return PlayerPrefs.GetInt("IsSoundOn") == 1;
            }

        }
        set
        {
            isSoundOn = value;
            PlayerPrefs.SetInt("IsSoundOn", isSoundOn ? 1 : 0);
        }
    }

    public enum EAudioClip
    {
        ButtonClick_SFX,
        FlyBonusStar_SFX,

        Shuffle_SFX,
        Answer_SFX,
        WrongWord_SFX,

        HintCoin_SFX,
        Character_SFX,
        LevelUp_SFX,
        NextRound_SFX,

        Disappear_SFX
    }

	// Use this for initialization
	void Start ()
	{
	    Instance = this;
	    
	    //PlayBgMusic();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayBgMusic()
    {
        if(IsMusicOn)
            musicAudioSource.Play();
    }

    public void PlaySound(EAudioClip clip)
    {
        if (IsSoundOn)
        {
            soundAudioSource.clip = audioClipList[(int) clip];
            soundAudioSource.Play();
        }
    }

    public void PlaySoundOnSetting(EAudioClip clip)
    {
        soundAudioSource.clip = audioClipList[(int)clip];
        soundAudioSource.Play();
    }

    private void UpdateMusicState()
    {
        if (IsMusicOn)
        {
            //musicAudioSource.Play();
        }
        else
        {
            //musicAudioSource.Stop();
        }
        //if(musicAudioSource.isPlaying)
        //    musicAudioSource.Stop();
        //else
        //    musicAudioSource.Play();
    }

}

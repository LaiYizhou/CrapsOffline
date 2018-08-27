using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    public static AudioControl Instance;

    [SerializeField] private List<AudioClip> audioClipList;
    [SerializeField] private List<AudioClip> spokenAudioClipList;

    [Space(10)]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundAudioSource;
    [SerializeField] private AudioSource spokenAudioSource;

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
        ButtonClick,

        GameSceneClick,

        DailyGiftSign,

        RollDice,
        
        WinChip,

        BetDownChip
    }

    public enum ESpokenAudioClip
    {
        _2_1,
        _2_no_point_1,
        
        _3_1,
        _3_no_point_1,

        _4_1,
        _4_hard_1,
        _4_no_point_1,
        _4_winner_1,

        _5_1,
        _5_no_point_1,
        _5_winner_1,

        _6_1,
        _6_hard_1,
        _6_no_point_1,
        _6_winner_1,

        _7_no_point_1,
        _7_out_1,

        _8_1,
        _8_hard_1,
        _8_no_point_1,
        _8_winner_1,

        _9_1,
        _9_no_point_1,
        _9_winner_1,

        _10_1,
        _10_hard_1,
        _10_no_point_1,
        _10_winner_1,

        _11_1,
        _11_no_point_1,

        _12_1,
        _12_no_point_1,

        comeOut

    }

	// Use this for initialization
	void Start ()
	{
	    Instance = this;
	    
	    PlayBgMusic();

	}

    public void PlayBgMusic()
    {
        if(IsMusicOn)
            musicAudioSource.Play();
    }

    public void StopBgMusic()
    {
        if(musicAudioSource.isPlaying)
            musicAudioSource.Stop();
    }

    public void PlaySpokenSound(ESpokenAudioClip clip)
    {
        if (IsSoundOn)
        {
            spokenAudioSource.clip = spokenAudioClipList[(int)clip];
            spokenAudioSource.Play();
        }
    }

    public void PlaySound(EAudioClip clip)
    {
        if (IsSoundOn)
        {
            soundAudioSource.clip = audioClipList[(int) clip];
            //Debug.Log("###" + (int)clip);
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
            musicAudioSource.Play();
        }
        else
        {
            musicAudioSource.Stop();
        }
       
    }

}

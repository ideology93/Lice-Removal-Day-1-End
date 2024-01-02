/*
Simple Sound Manager (c) 2016 Digital Ruby, LLC
http://www.digitalruby.com

Source code may no longer be redistributed in source format. Using this in apps and games is fine.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Be sure to add this using statement to your scripts
// using DigitalRuby.SoundManagerNamespace



public class AudioManager : GloballyAccessibleBase<AudioManager>
{
    public SoundManager SoundManager;
    public InputField SoundCountTextBox;
    public Toggle PersistToggle;

    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;

    public void PlaySound(int index)
    {
        // int count;
        // if (!int.TryParse(SoundCountTextBox.text, out count))
        // {
        //     count = 1;
        // }
        // while (count-- > 0)
        // {
        //     
        // }
        if (!SoundAudioSources[index].isPlaying)
        {
            SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
        }
        else return;
    }
    public void PlaySoundWithClipping(int index)
    {
         SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
    }
    public void StopSound(int index)
    {
        SoundManager.StopLoopingSound(SoundAudioSources[index]);
    }

    private void PlayMusic(int index)
    {
        MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, true);
    }

    private void CheckPlayKey()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySound(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaySound(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaySound(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlaySound(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlaySound(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlaySound(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlaySound(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayMusic(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayMusic(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayMusic(2);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayMusic(3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.LogWarning("Reloading level");

            if (!PersistToggle.isOn)
            {
                SoundManager.StopAll();
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        CheckPlayKey();
    }



    public void PersistToggleChanged(bool isOn)
    {
        SoundManager.StopSoundsOnLevelLoad = !isOn;
    }
}

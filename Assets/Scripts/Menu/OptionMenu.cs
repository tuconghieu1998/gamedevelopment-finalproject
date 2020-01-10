using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public ResItem[] resolutions;
    private int selectedResolution = 0;

    public Text resolutionLabel;

    public Toggle fullScreenTog, vsyncTog;

    public AudioMixer theMixer;

    public Slider masterSlider, musicSlider, sfxSlider;
    public Text masterLabel, musicLabel, sfxLabel;

    public AudioSource sfxLoop;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;

        vsyncTog.isOn = QualitySettings.vSyncCount != 0 ? true : false;

        bool isFound = false;
        // checking resolution
        for (int i = 0; i < resolutions.Length; ++i)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                selectedResolution = i;
                isFound = true;
                UpdateResLabel();
                break;
            }
        }

        if (!isFound)
        {
            resolutionLabel.text = Screen.width.ToString() + 'x' + Screen.height.ToString();
        }

        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }

        if (PlayerPrefs.HasKey("SFXVol"))
        {
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
        }
        masterLabel.text = (masterSlider.value + 80).ToString();
        musicLabel.text = (musicSlider.value + 80).ToString();
        sfxLabel.text = (sfxSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyGraphics()
    {
        // fullscreen
        Screen.fullScreen = fullScreenTog.isOn;

        QualitySettings.vSyncCount = vsyncTog.isOn ? 1 : 0;


        ResItem res = resolutions[selectedResolution];
        Screen.SetResolution(res.horizontal, res.vertical, fullScreenTog.isOn);
    }

    public void PrevRes()
    {
        if (selectedResolution > 0)
        {
            selectedResolution--;
            UpdateResLabel();
        }
    }

    public void NextRes()
    {
        if (selectedResolution < resolutions.Length - 1)
        {
            selectedResolution++;
            UpdateResLabel();
        }
    }
    

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + 'x' + resolutions[selectedResolution].vertical.ToString();
    }

    public void SetMasterValue()
    {
        masterLabel.text = (masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void SetMusicValue()
    {
        musicLabel.text = (musicSlider.value + 80).ToString();
        theMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXValue()
    {
        sfxLabel.text = (sfxSlider.value + 80).ToString();
        theMixer.SetFloat("SFXVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }

    public void PlaySFXLoop()
    {
        sfxLoop.Play();
    }
    public void StopSFXLoop()
    {
        sfxLoop.Stop();
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
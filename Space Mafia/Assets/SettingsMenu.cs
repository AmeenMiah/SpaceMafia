using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Slider HorzSenSlider;
    public Slider VolumeSlider;
    public Dropdown QualityDropdown;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("HorSen") > 499 && PlayerPrefs.GetFloat("HorSen") < 1001)
        {
            HorzSenSlider.value = PlayerPrefs.GetFloat("HorSen");
        }

        if (PlayerPrefs.GetFloat("Volume") < 1 && -81 < PlayerPrefs.GetFloat("Volume"))
        {
            audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume"));
            VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        if (PlayerPrefs.GetInt("Quality") > -1 && PlayerPrefs.GetInt("Quality") < 4)
        {
            QualityDropdown.value = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResoltuionIndex = 0;
        for (int i = 0; i <resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResoltuionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResoltuionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality (int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
        PlayerPrefs.SetInt("Quality", QualityIndex);
        PlayerPrefs.Save();
    }

    public void SetFullScreen (bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }
    
    public void SetResoultion(int ResoultionIndex)
    {
        Resolution resolution = resolutions[ResoultionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetHorziontalSensitivity(float HorSen)
    {
        PlayerPrefs.SetFloat("HorSen", HorSen);
        PlayerPrefs.Save();
        
    }


}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InGameSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public int index;

    Resolution[] resolutions;

    public Slider musicSlider, soundSlider, sensibilitySlider;

    public void Start()
    {
        /*audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;
        audioMixer.GetFloat("Sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;*/

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentReslutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                currentReslutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentReslutionIndex;
        SetResolution(currentReslutionIndex);
        resolutionDropdown.RefreshShownValue();
        sensibilitySlider.value = GameObject.FindGameObjectWithTag("Player" + (index + 1)).GetComponent<PlayerMovement>().rotationSensibility;

        Screen.fullScreen = true;
    }

    /*public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sound", volume);
    }*/

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution()
    {
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetResolution(int _index)
    {
        Resolution resolution = resolutions[_index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetSensibility(float sensibility)
    {
        GameObject.FindGameObjectWithTag("Player" + (index + 1)).GetComponent<PlayerMovement>().rotationSensibility = sensibility;
    }
}

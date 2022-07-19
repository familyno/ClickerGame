using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("ELEMENT CONTROL")]
    [SerializeField] private Dropdown _resolutionsDropdown;
    [SerializeField] private Dropdown _qualityDropdown;
    [SerializeField] private Slider _sliderMouseSens;

    public static float sensitivityMouse = 0.5f;
    public static bool callInGame;
    
    private Resolution[] resolutions;
    private bool fullscreen;

    private void OnEnable()
    {
        _sliderMouseSens.value = sensitivityMouse;
        // Resolution Dropdown
        resolutions = Screen.resolutions;

        _resolutionsDropdown.ClearOptions();

        List<string> optionsResolutions = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string optionResolution = resolutions[i].width + "x" + resolutions[i].height;
            optionsResolutions.Add(optionResolution);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i - 2;
            }
        }

        _resolutionsDropdown.AddOptions(optionsResolutions);
        _resolutionsDropdown.value = currentResolutionIndex;
        _resolutionsDropdown.RefreshShownValue();
        //End Resolution Dropdown


        // Quality Dropdown
        string[] qualityNames;


        qualityNames = QualitySettings.names;

        _qualityDropdown.ClearOptions();

        List<string> optionsQuality = new List<string>();

        for (int i = 0; i < qualityNames.Length; i++)
        {
            string option = qualityNames[i];
            optionsQuality.Add(option);
        }

        _qualityDropdown.AddOptions(optionsQuality);
        int qualityLevel = QualitySettings.GetQualityLevel();
        _qualityDropdown.value = qualityLevel;
        _qualityDropdown.RefreshShownValue();
        // END Quality Dropdown

        SetFullscreen(fullscreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreen = isFullscreen;
    }
}

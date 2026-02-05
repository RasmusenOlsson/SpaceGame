using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;

    private List<Resolution> uniqueResolutions = new List<Resolution>();

    private void OnEnable()
    {
        // Volume
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        PopulateResolutions();
    }

    private void PopulateResolutions()
    {
        if (resolutionDropdown == null)
        {
            Debug.LogError("SettingsMenu: TMP resolutionDropdown is NOT assigned!", this);
            return;
        }

        resolutionDropdown.onValueChanged.RemoveListener(SetResolution);

        Resolution[] all = Screen.resolutions;

        uniqueResolutions.Clear();
        List<string> options = new List<string>();

        for (int i = 0; i < all.Length; i++)
        {
            bool exists = false;
            for (int j = 0; j < uniqueResolutions.Count; j++)
            {
                if (uniqueResolutions[j].width == all[i].width &&
                    uniqueResolutions[j].height == all[i].height)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                uniqueResolutions.Add(all[i]);
                options.Add($"{all[i].width} x {all[i].height}");
            }
        }

        if (uniqueResolutions.Count == 0)
        {
            var cur = Screen.currentResolution;
            uniqueResolutions.Add(cur);
            options.Add($"{cur.width} x {cur.height}");
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        int currentIndex = 0;
        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            if (uniqueResolutions[i].width == Screen.width &&
                uniqueResolutions[i].height == Screen.height)
            {
                currentIndex = i;
                break;
            }
        }

        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        Debug.Log($"Loaded {uniqueResolutions.Count} resolutions", this);
    }

    public void SetVolume(float value) => AudioListener.volume = value;

    public void SetResolution(int index)
    {
        if (index < 0 || index >= uniqueResolutions.Count) return;
        var r = uniqueResolutions[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);

        Debug.Log($"TRY SetResolution -> {r.width}x{r.height} fullscreen={Screen.fullScreen}");

        Screen.SetResolution(r.width, r.height, Screen.fullScreen);

        Debug.Log($"AFTER -> Screen is now {Screen.width}x{Screen.height}");
    }
}

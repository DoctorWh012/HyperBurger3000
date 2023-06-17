using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [Header("Components")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivitySliderText;

    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private TextMeshProUGUI mainVolumeSliderText;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeSliderText;

    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vSyncToggle;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    private int desiredResolutionIndex;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadFromJson();

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = $"{filteredResolutions[i].width}x{filteredResolutions[i].height} {filteredResolutions[i].refreshRate}Hz ";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetDesiredResolutionIndex(int index)
    {
        desiredResolutionIndex = index;
    }

    public Resolution GetAndSetResolution(bool fullScreen)
    {
        Resolution resolution = filteredResolutions[desiredResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullScreen);
        return resolution;
    }


    public void UpdateSliderValue(TextMeshProUGUI sliderText)
    {

        if (sliderText == sensitivitySliderText)
        {
            sliderText.text = sensitivitySlider.value.ToString("#.00");
            return;
        }
        if (sliderText == mainVolumeSliderText)
        {
            sliderText.text = mainVolumeSlider.value.ToString("#.00");
            return;
        }
        if (sliderText == musicVolumeSliderText)
        {
            sliderText.text = musicVolumeSlider.value.ToString("#.00");
            return;
        }
    }

    public void SaveAndApply()
    {
        SaveToJson();
        LoadFromJson();
        if (UIManager.Instance == null) return;
        PlayerCam.Instance.GetSensitivity();
    }

    public void CreateJson()
    {
        PlayerPreferences playerPrefs = new PlayerPreferences();

        playerPrefs.resHeight = Screen.currentResolution.height;
        playerPrefs.resWidth = Screen.currentResolution.width;
        playerPrefs.sensitivity = 20;
        playerPrefs.mainVolume = 1;
        playerPrefs.musicVolume = 0.5f;
        playerPrefs.fullScreen = fullScreenToggle.isOn;
        playerPrefs.vSync = vSyncToggle.isOn;

        string json = JsonUtility.ToJson(playerPrefs, true);
        File.WriteAllText($"{Application.dataPath}/PlayerPrefs.json", json);
    }

    public void SaveToJson()
    {
        PlayerPreferences playerPrefs = new PlayerPreferences();

        Resolution savedRes = GetAndSetResolution(fullScreenToggle.isOn);

        playerPrefs.resHeight = savedRes.height;
        playerPrefs.resWidth = savedRes.width;
        playerPrefs.sensitivity = sensitivitySlider.value;
        playerPrefs.mainVolume = mainVolumeSlider.value;
        playerPrefs.musicVolume = musicVolumeSlider.value;
        playerPrefs.fullScreen = fullScreenToggle.isOn;
        playerPrefs.vSync = vSyncToggle.isOn;

        string json = JsonUtility.ToJson(playerPrefs, true);
        File.WriteAllText($"{Application.dataPath}/PlayerPrefs.json", json);
    }

    public void LoadFromJson()
    {
        if (!System.IO.File.Exists($"{Application.dataPath}/PlayerPrefs.json")) CreateJson();

        string json = File.ReadAllText($"{Application.dataPath}/PlayerPrefs.json");
        PlayerPreferences playerPrefs = JsonUtility.FromJson<PlayerPreferences>(json);

        sensitivitySlider.value = playerPrefs.sensitivity;
        UpdateSliderValue(sensitivitySliderText);

        mainVolumeSlider.value = playerPrefs.mainVolume;
        AudioListener.volume = playerPrefs.mainVolume;
        UpdateSliderValue(mainVolumeSliderText);

        musicVolumeSlider.value = playerPrefs.musicVolume;
        musicSource.volume = playerPrefs.musicVolume;
        UpdateSliderValue(musicVolumeSliderText);

        Screen.SetResolution(playerPrefs.resWidth, playerPrefs.resHeight, playerPrefs.fullScreen);
        fullScreenToggle.isOn = playerPrefs.fullScreen;

        vSyncToggle.isOn = playerPrefs.vSync;
        if (playerPrefs.vSync) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
    }
}


[System.Serializable]
public class PlayerPreferences
{
    public int resHeight;
    public int resWidth;
    public float sensitivity;
    public float mainVolume;
    public float musicVolume;
    public bool vSync;
    public bool fullScreen;
}


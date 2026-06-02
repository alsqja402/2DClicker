using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;

    [Header("Setting UI")]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private const string FULL_SCREEN_KEY = "FullScreen";
    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";

    private void Start()
    {
        LoadSettings();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettingPanel()
    {
        settingPanel.transform.localScale = Vector3.zero;

        settingPanel.SetActive(true);
        settingPanel.transform.DOScale(1f, 0.2f);
    }

    public void CloseSettingPanel()
    {
        settingPanel.transform.DOScale(0f, 0.2f)
        .OnComplete(() =>
        {
            settingPanel.SetActive(false);
        });
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    // 패널 설정들
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        PlayerPrefs.SetInt(FULL_SCREEN_KEY, isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        bool isFullScreen = PlayerPrefs.GetInt(FULL_SCREEN_KEY, 1) == 1;
        float bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.7f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.8f);

        Screen.fullScreen = isFullScreen;

        if (fullScreenToggle != null)
        {
            fullScreenToggle.isOn = isFullScreen;
        }

        if (bgmSlider != null)
        {
            bgmSlider.value = bgmVolume;
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
        }
    }
}
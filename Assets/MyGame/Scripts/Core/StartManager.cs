using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[AddComponentMenu("XuanTien/StartManager")]

public class StartManager : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelOption;
    public Slider sliderMusic;
    public Slider sliderSFX;
    // Start is called before the first frame update
    void Start()
    {
        GetDataVolume();
        panelMain.SetActive(true);
        panelOption.SetActive(false);
    }
    void GetDataVolume()
    {
        sliderMusic.value = DataManager.DataMusic;
        sliderSFX.value = DataManager.DataSFX;
    }
    public void OnOptionClick()
    {
        panelMain.SetActive(false);
        panelOption.SetActive(true);
    }
    public void OnOptionClickExit()
    {
        panelMain.SetActive(true);
        panelOption.SetActive(false);
    }
    public void OnPlayClick(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void SetMusicVolume(float volume)
    {
        DataManager.DataMusic = volume;
        AudioManager.Instance.SetMusicVolume(volume);
    }
    public void SetSFXVolume(float volume)
    {
        DataManager.DataSFX = volume;
        AudioManager.Instance.SetSfxVolume(volume);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("https://unity.com/")
#else
        Application.Quit();
#endif
    }
}

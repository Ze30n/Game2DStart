using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
   public static int DataCoin
    {
        get => PlayerPrefs.GetInt("Key",0);     //Lấy gtri
        set => PlayerPrefs.SetInt("Key", value);//Đặt gtri trong ConstantKey.KeyCoinID = value
    }

    public static float DataMusic
    {
        get => PlayerPrefs.GetFloat("Music", 1);
        set => PlayerPrefs.SetFloat("Music", value);
    }

    public static float DataSFX
    {
        get => PlayerPrefs.GetFloat("SFX", 1);
        set => PlayerPrefs.SetFloat("SFX", value);
    }
    
}

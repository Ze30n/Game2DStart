using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[AddComponentMenu("XuanTien/UIManager")]

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textCoin;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.coinEventUpdate.AddListener(AddCoinUI);        //Nhận sự kiện đã phát đi trong <GameManager>
        textCoin.text = DataManager.DataCoin.ToString();                    //Load coin khi vào game
    }

    private void AddCoinUI(int coin)
    {
        textCoin.text = coin.ToString();        //Ghi sự kiệ lên màn hình UI
    }

    
    //void Update()
    //{
        //textCoin.text = GameManager.Instance.GetCoin().ToString();      //Hiện gtri lên UI
    //}
}

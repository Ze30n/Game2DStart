using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[AddComponentMenu("XuanTien/GameManager")]

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get => instance;
    }
    public static GameManager instance;
    private int coin;

    public UnityEvent<int> coinEvent;      //Khai báo biến coinEvent của <UnityEvents> sử dụng thắng k phải truyền qua EventManager
    public UnityEvent<int> coinEventUpdate;//Khai báo này để sử dụng phát cho UI để k bị lặp với biến <coinEvent>
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        if (coinEvent == null)      //Nếu coinEvent = null thì tạo ra 1 đối tượng mới nó sẽ phát đi sự kiện
        {
            coinEvent = new UnityEvent<int>();
        }
        if (coinEventUpdate == null)      
        {
            coinEventUpdate = new UnityEvent<int>();
        }
    }
    private void Start()
    {
        this.coin = DataManager.DataCoin;       //Lấy gtri coin từ DataManager.DataCoin
        coinEvent.AddListener(AddCoin);         //Nhận gtri phát event từ <Player> truyền vào AddCoin
        
    }

    public void AddCoin(int coin)
    {
        this.coin += coin;
        DataManager.DataCoin = this.coin;       //Truyền gtri lưu vào <static DataManager>
        coinEventUpdate?.Invoke(this.coin);      //Phát đi sự kiện (this.coin) => phát đến <UIManager>
    }
    //public int GetCoin()
    //{
    //    return coin;
    //}
}

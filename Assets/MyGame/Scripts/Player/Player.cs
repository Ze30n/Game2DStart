using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("XuanTien/Player")]

public class Player : MonoBehaviour, DamageTakenTimes
{
    public int addCoin = 1;
    public float maxHealth = 100;
    public AudioClip coinClip;
    [HideInInspector]
    public bool isDead = false;

    private float curHealth;

    void Start()
    {
        curHealth = maxHealth;
    }
    public void TakeDamage(int damage, Vector2 force, GameObject instigattor)
    {
        if (isDead) return;
        curHealth -= damage;
        if (curHealth <= 0)
        {
            curHealth = 0;
            isDead = true;
            //anim.SetTrigger(dyingAnimation);
            gameObject.SetActive(false);          //Ẩn đối tượng và tải cảnh khác
            //Destroy(gameObject, 0.8f);              //Sẽ tạo lỗi
        }   //Khi máu hiện tại nhỏ hơn = 0 thì gán máu = 0 và chuyển biến <isDead> = true và phá hủy đối tượng sau 0.8s
    //Sau khi ng chơi chết: Load cảnh khác/ Load UI / Load Player mới
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")        //Va chạm với tag <Coin> sẽ tăng gtri
        {
            GameManager.Instance.coinEvent?.Invoke(addCoin);        //Khi va chạm thì phát event về <coinEvent>
            AudioManager.Instance.PlaySfx(coinClip);
            Destroy(collision.gameObject);
        }
    }
}

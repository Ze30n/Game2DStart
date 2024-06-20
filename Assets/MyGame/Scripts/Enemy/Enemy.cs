using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, DamageTakenTimes
{
    #region Public
    [Header("FX")]
    public GameObject hurtEfect;
    [HideInInspector]
    public bool isDead = false;
    [Header("MaxHealth")]
    public float maxHealth = 100;
    public float nextTime = 0;
    public float rateTime = 1.4f;
    public int damageToGive = 20;
    public Vector2 force;
    #endregion
    #region Private
    private float currHealth;
    private int dyingAnimation;
    private int attackingAnimation;
    private Animator anim;
    private Player player;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        dyingAnimation = Animator.StringToHash("Dying");
        attackingAnimation = Animator.StringToHash("Attacking");
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage, Vector2 force, GameObject instigattor)
    {
        if (isDead) { return; }
        currHealth -= damage;
        if (hurtEfect != null)
        {   //Khi khác null thì khởi tạo và truyền vào hiệu ứng <hurtEfect>, vị trí người chơi <instigattor>
            Instantiate(hurtEfect, instigattor.transform.position, Quaternion.identity);
        }
        if (currHealth <= 0)
        {
            currHealth = 0;
            isDead = true;
            anim.SetTrigger(dyingAnimation);
            Destroy(gameObject, 0.8f);
        }   //Khi máu hiện tại nhỏ hơn = 0 thì gán máu = 0 và chuyển biến <isDead> = true và phá hủy đối tượng sau 0.8s
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.isDead) return;
        if (player == null) return;
        if (collision.CompareTag("Player"))
        {
            if (Time.time > nextTime)
            {
                nextTime = Time.time + rateTime;
                anim.SetTrigger(attackingAnimation);
                player.TakeDamage(damageToGive, force,gameObject);
            }
        }
    }
}

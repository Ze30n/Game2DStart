using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("XuanTien/PlayerAttack")]

public class PlayerAttack : MonoBehaviour
{
    #region Public
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public float radiusAttack = 0.2f;
    public float attackRate = 0.2f;
    public float nextAttack = 0f;
    public float timerDelay = 0.2f;
    public int damageToGive = 50;
    public Vector2 force;
    #endregion
    #region Private
    private Animator anim;          //Khởi tạo hoạt ảnh
    private int attackAnimation;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren <Animator>();        //Truyền hoạt ảnh
        attackAnimation = Animator.StringToHash("Attacking");       //Truyền tín hiệu chuyển đổi hoạt ảnh trong <Animator>
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger(attackAnimation);       //Kích hoạt điều kiện chuyển hoạt ảnh
            GetKeyR();
        }
    }
    private bool GetKeyR()
    {   //Khi bấm (R) tấn công thì sau 1 khoảng tg Time > nexAttack thì nó mới thực hiện tấn công tiếp
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            StartCoroutine(Attack(timerDelay));
            return true;
        } else return false;
    }
    IEnumerator Attack(float delay)
    {
        yield return new WaitForSeconds(delay);         //Sau bao nhiều giây thì thực hiện hành động
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttack, enemyLayer);

        foreach (var enemy in hitEnemies)               //Duyệt qua các phần tử truyền gtri vào interface
        {
            enemy.GetComponent<DamageTakenTimes>().TakeDamage(damageToGive, force, gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, radiusAttack);
        }
    }
}

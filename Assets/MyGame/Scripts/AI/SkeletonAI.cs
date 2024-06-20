using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("XuanTien/SkeletonAI")]

public class SkeletonAI : MonoBehaviour
{
    #region Public
    public float patrolSpeed = 1f;
    public float MinDistance = 0.1f;
    public float chaseRange = 4f;
    public float chaseSpeed = 2.2f;
    public Transform pointA;
    public Transform pointB;
    #endregion
    #region Private
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;      //Lấy 1 đối tượng ng chơi
    private int StandingAnimation;
    private int ChasingAnimation;
    private bool Chasing = false;
    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        StandingAnimation = Animator.StringToHash("Standing");
        ChasingAnimation = Animator.StringToHash("Chasing");
        transform.position = pointA.position;       //Bắt đầu thì Enemy = point A
        target = pointB;
        player = GameObject.FindGameObjectWithTag("Player");        //Gán đối tượng theo tag Player (Chú ý treo tag vào Chracter)
    }

    // Update is called once per frame
    void Update()
    {
        //Kiểm tra khoảng các vị trí của Enemy với vị trí của ng chơi 
        float distanceWithPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceWithPlayer < chaseRange)        //Nếu khoảng cách nhỏ hơn <chaseRange> thì sẽ đuổi theo
        {
            Chasing = true;
        } else
        {
            Chasing = false;
        }

        if (Chasing)
        {
            Chase();            
        } else
        {
            Patrol();
        }
    }

    private void Chase()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;        //<normalized> chuẩn hóa vector, dùng tạo 1 vector mới k ảnh hưởng với vector cũ
        rb.velocity = direction * chaseSpeed;
        anim.SetTrigger(ChasingAnimation);
    }

    void Patrol()
    {   
        //Nếu hành động hiện tại là "Skeleton" < ANIMATION NAME > thì thực hiện hết rồi chuyển tiếp
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton"))     //Tg thực hiện = Exit time
        {
            return;
        }
        //Xét khoảng các giữ Enemy và Mục tiêu quá nhỏ
        if (Vector2.Distance(transform.position, target.position) < MinDistance)
        {
            target = target == pointA ? pointB : pointA;        //Nếu mục tiêu = A thì đến B và ngc lại
            anim.SetTrigger(StandingAnimation);
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        //if (transform.position == pointA.position)      //FlipX của Enemy trc
        //{
        //    Vector2 scale = transform.localScale;
        //    scale.x *= -1;
        //    transform.localScale = scale;
        //    target = pointB;
        //    anim.SetTrigger(StandingAnimation);
        //} else if (transform.position == pointB.position)
        //{
        //    Vector2 scale = transform.localScale;
        //    scale.x *= -1;
        //    transform.localScale = scale;
        //    target = pointA;
        //    anim.SetTrigger(StandingAnimation);
        //}

        //Vecto hướng sẽ bằng vị trí mục tiêu <target> trừ đi vị Enemy 
        //Vector2 direction = (target.position - transform.position).normalized;
        //rb.velocity = direction * moveSpeed;        //Sử dụng vector hướng để di chuyển với tốc độ có sẵn
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed*Time.deltaTime);
    }
}

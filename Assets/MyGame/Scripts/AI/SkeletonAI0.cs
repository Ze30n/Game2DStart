using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("XuanTien/SkeletonAI0")]
public class SkeletonAI0 : MonoBehaviour
{
    #region Public
    public Transform pointA;
    public Transform pointB;
    public float speedPatrol = 1f;
    public float speed = 1f;
    public float speedChasing = 2.2f;
    public float MinDistance = 0.2f;
    public float baseIdleTime = 2f;
    public float idleTime;
    public float chaseRange = 3f;
    #endregion
    #region Private
    private int standingAnimation = Animator.StringToHash("Standing");
    //private bool isWalk = false;
    private bool isChasing = false;
    private GameObject player;      //Khởi tạo <GameObject> <player>
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Enemy enemy;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        transform.position = pointA.position;
        target = pointB;
        idleTime = baseIdleTime;
        player = GameObject.FindGameObjectWithTag("Player");        //Gán gtri cho <player> = cách tìm tag đã gắn vào ng chơi
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isDead) { return; }
        Movement();     
    }

    void Movement()
    {
        //Ktra vị trí của <Enemy> so với vị trí của ng chơi đã truyền vào
        float distancePlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distancePlayer < chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            Chasing();
        }
        else
        {
            Partrol();
        }

        Vector2 scale = transform.localScale;
        float x = player.transform.position.x - transform.position.x;
        if (x < 0 && isChasing)
        {
            scale.x = -(Math.Abs(scale.x));
        }
        else if (x > 0 && isChasing)
        {
            scale.x = (Math.Abs(scale.x));
        }
        if ((transform.position.x - pointA.position.x < 0) && !isChasing)
        {
            scale.x = (Math.Abs(scale.x));
        }
        else if ((transform.position.x - pointB.position.x > 0) && !isChasing)
        {
            scale.x = -(Math.Abs(scale.x));
        }
        //else if ((transform.position.x - pointA.position.x > 0) && (transform.position.x - pointB.position.x < 0) && !isChasing)
        //{
        //    scale.x = (Math.Abs(scale.x));
        //}
        transform.localScale = scale;

        IdleAnimation();
    }
    private void Chasing()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction*speedChasing;
        anim.SetBool(standingAnimation, false);
        //transform.LookAt(direction);
    }

    private void Partrol()
    {
        //Nếu hành động hiện tại là "Skeleton" < ANIMATION NAME > thì thực hiện hết rồi chuyển tiếp
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton"))     //Tg thực hiện = Exit time
        //{
        //    return;
        //}

        if (Vector2.Distance(transform.position, target.position) < MinDistance)
        {
            target = target == pointB ? pointA : pointB;
            anim.SetBool(standingAnimation, true);

            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            speedPatrol = 0;
            IdleAnimation();
            //isWalk = false;
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speedPatrol;
    }
    void IdleAnimation()
    {
            idleTime -= Time.deltaTime;
            if (idleTime <= 0)
            {
                anim.SetBool(standingAnimation, false);
                idleTime = baseIdleTime;
                speedPatrol = speed;
                //isWalk = true;
            }
    }
}
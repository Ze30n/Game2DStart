using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[AddComponentMenu("XuanTien/PlayerController")]

public class PlayerController : MonoBehaviour
{
    #region Public
    public LayerMask groundLayer;
    #endregion
    #region Private
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] Transform groundCheck;
    private Rigidbody2D rb;
    private SpriteRenderer spriteChracter;
    private bool onGround = false;
    private bool facingRight = true;
    private int walkingAnimation = Animator.StringToHash("Walking");
    private int jumpingAnimation = Animator.StringToHash("Jumping");
    private Animator anim;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteChracter = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //Vẽ 1 đg tròn với tâm là <groundCheck> bán kính 0.2 để check xem nó có va chạm <groundLayer> k
        onGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (!onGround)          //Input.GetKeyUp(KeyCode.Space)
        {
            NotJump();
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);            //Nhảy lên: di chuyển theo trục y. <rb.velocity.x> = giữ nguyên x
        anim.SetBool(jumpingAnimation, true);
    }
    private void NotJump()
    {
        anim.SetBool(jumpingAnimation, false);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");                     //Di chuyển theo <vector 2>*tốc độ và <rb.velocity>.y = giữ nguyên trục y
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        if ((horizontal>0 && !facingRight)||(horizontal<0 && facingRight))
        {
            Flip();
        }
        if (math.abs(horizontal) > 0)
        {
            anim.SetBool(walkingAnimation, true);
        }
        else
        {
            anim.SetBool(walkingAnimation, false);
        }
    }
    
    private void Flip()
    {
        facingRight = !facingRight;         //<facingRight> = Đảo của <facingRight> (Dòng này để phân định chiều quay trái phải riêng biệt)
        //spriteChracter.flipX = !facingRight;      //Lật
        Vector2 scale = transform.localScale;       //Lấy tỉ lệ gán
        scale.x *= -1;                              //Trục x*(-1) để đảo chiều
        transform.localScale = scale;               //Gán lại
        //Như này thì nó sẽ đảo chiều tất cả những thứ set up liên quan đến ng chơi >< Cách <facingRight> bên trên chỉ đảo lại <spritePlayer>
    }
}

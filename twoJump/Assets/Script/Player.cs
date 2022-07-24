using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;

    public float speed, jumpForce;
    public Transform layerCheck;
    public  LayerMask layerMask;

    public bool isJump, isLayer,isDash;

    private bool jumpPressed;
    private  int jumpCount;
    private float hor;

    [Header("Dash")]
    public float dashTime;//��Ծʱ��
    private float dashTimeLeft;//ʣ��ʱ��
    private float lastDashTime=-10; //��һ��dashʱ���
    public float dashCoolDown;
    public float dashSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")&&jumpCount>0)
        {
            jumpPressed = true;
        }
        //���
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (Time.time >= (dashCoolDown + lastDashTime))
            {
                //����ִ��dash
                ReadyToDash();
            }
        }
    }

    private void FixedUpdate()
    {
        isLayer = Physics2D.OverlapCircle(layerCheck.transform.position,0.1f,layerMask);
        Movenemt();
        Jump();
        SwitchAnimation();
        //��Ծ
        Dash();
    }

    private void Movenemt()
    {
        hor = Input.GetAxisRaw("Horizontal");
        rigidbody2d.velocity = new Vector2(hor*speed, rigidbody2d.velocity.y);

        if (hor != 0)
        {
            transform.localScale = new Vector3(hor, 1, 1);
        }
    }

    private void Jump()
    {
        if (isLayer)
        {
            isJump = false;
            jumpCount = 2;
        }
        if (jumpPressed && isLayer)
        {
            isJump = true;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }else if (jumpPressed && isJump && jumpCount>0)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    private void SwitchAnimation()
    {

        animator.SetFloat("isRun", Mathf.Abs(rigidbody2d.velocity.x));

        if (isLayer)
        {
            animator.SetBool("luo", false);
            animator.SetBool("isIdel", true);
        }
        else if (!isLayer && rigidbody2d.velocity.y > 0)
        {
            animator.SetBool("isIdel", false);
            animator.SetBool("isJump", true);
        }
        else if( rigidbody2d.velocity.y < 0)
        {
            animator.SetBool("isJump", false);
            animator.SetBool("luo", true);
        }
    }

    private void ReadyToDash()
    {

        isDash = true;

        dashTimeLeft = dashTime;

        lastDashTime = Time.time;

    }

    private void Dash()
    {
        if (isDash)
        {

            if (dashTimeLeft > 0)
            {

                if (rigidbody2d.velocity.y > 0&& isLayer)
                {
                    rigidbody2d.velocity = new Vector2(dashSpeed * hor, jumpForce);
                }
                rigidbody2d.velocity = new Vector2(dashSpeed * hor, rigidbody2d.velocity.y);

                dashTimeLeft -= Time.deltaTime;

                ShadowPool.Instance.DequeueS();
            }
            if (dashTimeLeft <= 0)
            {
                isDash = false;
                if (!isLayer)
                {
                    //Ŀ��Ϊ���ڿ��н��� Dash ��ʱ����Խ�һ��С��Ծ�������Լ���Ҫ����ɾ������
                    rigidbody2d.velocity = new Vector2(dashSpeed * hor, jumpForce);
                }
            }
        }

    }
}

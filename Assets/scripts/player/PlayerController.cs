using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CheckGround checkground;
    [SerializeField] float speed;
    [SerializeField] float gravity;
    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpHeight;

    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //設置判定
        isGround = checkground.getIsGround();

        //float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float ySpeed = -gravity;

        xSpeed = Input.GetAxis("Horizontal") * speed;
        float verticalKey = Input.GetAxis("Vertical");
        if (isGround)
        {
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                //ジャンプした位置を保存
                jumpPos = transform.position.y;
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }
        else if(isJump)
        {
            //上ボタンを押されている。かつ、現在の高さがジャンプした位置から自分の決めた位置より下ならジャンプを継続する
            if (verticalKey > 0　&& jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
            }
            else
            {
                isJump = false;
            }
        }
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
}

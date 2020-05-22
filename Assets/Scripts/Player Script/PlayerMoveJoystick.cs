using UnityEngine;
using System.Collections;

public class PlayerMoveJoystick : MonoBehaviour {


    public float speed = 8f;
    public float maxVelocity = 4f;
    
    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft, moveRight;

    void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        if (moveLeft)
        {
            MoveLeft();
        }
        if (moveRight)
        {
            MoveRight();
        }

    }

    public void SetMoveLeft(bool moveLeft)
    {
        this.moveLeft = moveLeft;
        moveRight = !moveLeft;
    }

    public void StopMoving()
    {
        moveLeft = moveRight = false;
        anim.SetBool("Walk", false);
    }

    void MoveLeft()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);
        
        //calculate speed
        if (vel < maxVelocity)
            forceX = -speed;


        // change direction
        Vector3 temp = transform.localScale;
        temp.x = -1f;
        transform.localScale = temp;
        // activate animation
        anim.SetBool("Walk", true);
        myBody.AddForce(new Vector2(forceX, 0));
    }

    void MoveRight()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        //calculate speed
        if (vel < maxVelocity)
            forceX = speed;


        // change direction
        Vector3 temp = transform.localScale;
        temp.x = 1f;
        transform.localScale = temp;
        // activate animation
        anim.SetBool("Walk", true);
        myBody.AddForce(new Vector2(forceX, 0));

    }
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 8f;
    public float maxVelocity = 4f;

    [SerializeField]
    private Rigidbody2D myBody;
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        PlayerMoveKeyboard();	
	}

    void PlayerMoveKeyboard()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        float h = Input.GetAxisRaw("Horizontal");
        
        if(h > 0) //Going right
        {
            //calculate speed
            if (vel < maxVelocity)
                forceX = speed;

            // activate animation
            anim.SetBool("Walk", true);

            // change direction
            Vector3 temp = transform.localScale;
            temp.x = Mathf.Abs(temp.x);
            transform.localScale = temp;
            
        } else if (h < 0)
        {
            //calculate speed
            if (vel < maxVelocity)
                forceX = -speed;

            // activate animation
            anim.SetBool("Walk", true);

            // change direction
            Vector3 temp = transform.localScale;
            temp.x = -Mathf.Abs(temp.x);
            transform.localScale = temp;

        } else
        {
            // deactivate animation
            anim.SetBool("Walk", false);
        }

        // move the player
        myBody.AddForce(new Vector2(forceX, 0));
    }

}

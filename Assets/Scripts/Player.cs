using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    //State
    bool isAlive = true;

    //Cached components references
    Rigidbody2D myrigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    float gravityScaleAtStart;

    //Messages then methods
	// Use this for initialization
	void Start () {
        myrigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = myrigidBody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
	}

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myrigidBody.velocity.y);
        myrigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myrigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myrigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocitiy = new Vector2(myrigidBody.velocity.x, controlThrow * climbSpeed);
        myrigidBody.velocity = climbVelocitiy;
        myrigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myrigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs (myrigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidBody.velocity.x), 1f);
        }
    }
}

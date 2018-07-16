using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myrigidBody;

	// Use this for initialization
	void Start () {
        myrigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsFacingRight())
        {
            myrigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myrigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myrigidBody.velocity.x)), 1f);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // public setting
    [SerializeField] public float speed = 5.0f;

    // Internal Data
    protected Rigidbody2D mRigidBody;

    // initialization
    void OnEnable()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); //移動
    }

    // Movement handling

    void MoveRigidBody(Vector2 moveDirVec)
    {

        Vector2 moveVec = moveDirVec * speed * Time.deltaTime;
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 newPos = myPos + moveVec;
       
        mRigidBody.MovePosition(newPos);
    }

    void HandleMovement()
    {
        Vector2 moveDirVec = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirVec = new Vector2(0, 3);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirVec = new Vector2(0, -3);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirVec = new Vector2(-3, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirVec = new Vector2(3, 0);
        }

        if (moveDirVec != Vector2.zero)
        {
            MoveRigidBody(moveDirVec);
        }
    }
}
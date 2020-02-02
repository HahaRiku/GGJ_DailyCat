using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    //enum State { Up, Down, Right, Left, Repair, Discard, Idle };

    //State state = State.Idle;

    bool isUp = false;
    bool isDown = false;
    bool isRight = false;
    bool isLeft = false;
    bool isRepair = false;
    bool isDiscard = false;

    //player moving range
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        anim = GetComponent<Animator>();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Move();
        Behaviorhandler();
    }
    private void Behaviorhandler()
    {
        if(isUp && !isDown)
        {
            SetPlayerMove(0, 1 * Time.deltaTime);
            SetPLayerAnimation(3, true);
        }
        else if (!isUp && isDown)
        {
            SetPlayerMove(0, -1 * Time.deltaTime);
            SetPLayerAnimation(0, true);
        }
        else if (isRight && !isLeft)
        {
            SetPlayerMove(1 * Time.deltaTime, 0);
            SetPLayerAnimation(1, true);
        }
        else if (!isRight && isLeft)
        {
            SetPlayerMove(-1 * Time.deltaTime, 0);
            SetPLayerAnimation(2, true);
        }
        else if(!(((isRight|| isLeft) || isDown) || isUp))
        {
            anim.SetBool("isWalking", false);
        }
    }



    private void SetPlayerMove(float deltaX, float deltaY)
    {
        deltaX *= moveSpeed;
        deltaY *= moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector3(newXPos, newYPos, -3);
    }

    private void SetPLayerAnimation(int dir,bool isWalking)
    {
        anim.SetInteger("dir", dir);
        anim.SetBool("isWalking", isWalking);
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector3(newXPos, newYPos, -3);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("dir", 3);
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetInteger("dir", 0);
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetInteger("dir", 2);
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetInteger("dir", 1);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void PlayerDo(string doThing,bool isPressed)
    {
        switch (doThing)
        {
            case "Up":
                if (isPressed)
                {
                    isUp = true;
                }
                else
                {
                    isUp = false;
                }
                break;
            case "Down":
                if (isPressed)
                {
                    isDown = true;
                }
                else
                {
                    isDown = false;
                }
                break;
            case "Right":
                if (isPressed)
                {
                    isRight = true;
                }
                else
                {
                    isRight = false;
                }
                break;
            case "Left":
                if (isPressed)
                {
                    isLeft = true;
                }
                else
                {
                    isLeft = false;
                }
                break;
            case "Repair":
                if (isPressed)
                {
                    isRepair = true;
                }
                else
                {
                    isRepair = false;
                }
                break;
            case "Discard":
                if (isPressed)
                {
                    isDiscard = true;
                }
                else
                {
                    isDiscard = false;
                }
                break;
            default:
                break;
        }
    }
}
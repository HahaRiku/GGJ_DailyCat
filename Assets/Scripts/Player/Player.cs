using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    bool isTouch = false;

    //player moving range
    float xMin;
    float xMax;
    float yMin;
    float yMax;


    private bool isFallDown = false;
    private bool isWorking = false;
    private bool canWorking = false;
    private bool canAttack = false;
    // Start is called before the first frame update

    private GameObject nowWorkbench = null;
    private HealthBar nowHealBar = null;
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
        if (!isFallDown)
        {
            //Move();
            Behaviorhandler();
        }

    }
    private void Behaviorhandler()
    {
        if (isRepair)
        {
            if (GameManager.manager.EndGame)
                GameManager.manager.reload();
            if (canWorking)
            {
                isWorking = true;
                anim.SetBool("isWalking", false);
                anim.SetBool("isWorking", true);
                SetPLayerFilpX(anim.GetInteger("dir"));
                return;
            }
        }
        else
        {
            isWorking = false;
            anim.SetBool("isWorking", false);
            SetPLayerFilpX(4);
        }
        if (isTouch)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isTouching", true);
            SetPLayerFilpX(anim.GetInteger("dir"));
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = !spriteRenderer.flipX;
            return;
        }
        else
        {
            anim.SetBool("isTouching", false);
            SetPLayerFilpX(4);
        }
        if (isUp && !isDown)
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
            SetPLayerAnimation(2, true);
        }
        else if (!isRight && isLeft)
        {
            SetPlayerMove(-1 * Time.deltaTime, 0);
            SetPLayerAnimation(1, true);
            
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
        //var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        //var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        //var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        //transform.position = new Vector3(newXPos, newYPos, -3);
        if (Input.GetKey(KeyCode.C))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isTouching", true);
            SetPLayerFilpX(anim.GetInteger("dir"));
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = !spriteRenderer.flipX;
            return;
        }
        else
        {
            anim.SetBool("isTouching", false);
            SetPLayerFilpX(4);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (canWorking)
            {
                isWorking = true;
                anim.SetBool("isWalking", false);
                anim.SetBool("isWorking", true);
                SetPLayerFilpX(anim.GetInteger("dir"));
                return;
            }          
        }
        else
        {
            isWorking = false;
            anim.SetBool("isWorking", false);
            SetPLayerFilpX(4);
        }
        if (Input.GetKey(KeyCode.X))
        {
            SetPLayerFilpX(anim.GetInteger("dir"));
            PlayerFallDown();
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            SetPlayerMove(0, 1 * Time.deltaTime);
            anim.SetInteger("dir", 3);
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            SetPlayerMove(0, -1 * Time.deltaTime);
            anim.SetInteger("dir", 0);
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            SetPlayerMove(1 * Time.deltaTime, 0);
            anim.SetInteger("dir", 2);
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            SetPlayerMove(-1 * Time.deltaTime, 0);
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
            case "Touch":
                if (isPressed)
                {
                    if (!isRepair)
                    {
                        isTouch = true;
                    }                    
                }
                else
                {
                    isTouch = false;
                }
                break;
            default:
                break;
        }
    }


    public void PlayerFallDown()
    {
        StartCoroutine(FallDown());
    }

    public bool GetPlayerFallDown()
    {
        return isFallDown;
    }

    public bool GetPlayerWorking()
    {
        return isWorking;
    } 

    IEnumerator FallDown()
    {
        if (!isFallDown)
        {
            canAttack = true;
            isFallDown = true;
            anim.SetBool("isWalking", false);
            anim.SetBool("isFallDown", true);
            yield return new WaitForSeconds(2);
            anim.SetBool("isWalking", false);
            anim.SetBool("isFallDown", false);
            SetPLayerFilpX(4);
            canAttack = false;
            isFallDown = false;
        }            
    }

    private void SetPLayerFilpX(int dir)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (dir)
        {
            case 0:
                spriteRenderer.flipX = true;
                break;
            case 1:
                spriteRenderer.flipX = true;
                break;
            case 2:
                spriteRenderer.flipX = false;
                break;
            case 3:
                spriteRenderer.flipX = false;
                break;
            case 4:
                spriteRenderer.flipX = false;
                break;
            default:
                spriteRenderer.flipX = false;
                break;


        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "antique")
        {
            canWorking = true;
            nowWorkbench = collision.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            nowHealBar = collision.gameObject.GetComponent<HealthBarCollision>().GethealthBar();
            if (canAttack)
            {
                collision.gameObject.GetComponent<HealthBarCollision>().GethealthBar().getLifeBarSystem().Damage(60);
                canAttack = false;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);        
        nowWorkbench = null;
        nowHealBar = null;
        if (collision.gameObject.tag == "antique")
        {
            canWorking = false;
        }           
    }

    public GameObject GetnowWorkbench()
    {
        return nowWorkbench;
    }
    public void DealnowWorkbenchDamage(int damage)
    {
        if(nowHealBar != null)
        {
            nowHealBar.getLifeBarSystem().Damage(damage);
        }        
    }
    public bool GetPlayerTouching()
    {
        return isTouch;
    }

}
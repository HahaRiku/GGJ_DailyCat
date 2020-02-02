using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    public enum Direction {
        上下,
        左右
    }
    public enum FourDirection {
        上,
        下,
        左,
        右
    }
    public enum Status {
        正方向,
        負方向
    }
    public enum RotationStatus {
        逆時針,
        順時針
    }
    private Direction dir;
    private float upOrRightMaxValue;    //up and right
    private float downOrLeftMaxValue;    //down and left
    private Status status;
    private float speed;

    private bool start = false;
    private bool bye = false;
    private FourDirection byeDir;

    private bool hello = false;
    private FourDirection helloDir;
    private float helloDestX;
    private float helloDestY;

    private bool collide = false;

    public Sprite yelloLight;
    public Sprite redLight;
    private GameObject perspectivePivot;
    private float rotationSpeed = 0.1f;
    private float rotationAngle = 0.0f;
    private float originalAngle = 0.0f;
    private RotationStatus rotationStatus = RotationStatus.逆時針;

    private GameManager GM;
    private GameObject Player;
    private Player playerComp;
    private float perspectiveRadius = 2.5f;

    private Animator ani;
    private bool detectPlayer = false;
    private GameObject detectPlayerObj;
    private bool attackPlayer = false;
    private bool goBack = false;

    public Sprite angryEmotion;
    public Sprite loveEmotion;
    private GameObject emotionPivot;
    private SpriteRenderer emotionSR;
    private Animator emotionAni;
    private bool angry = false;

    private bool checkPlayerDodge = false;

    private Coroutine angryStatusInstance;

    private bool detectWorkTable = false;
    private GameObject detectWorkTableObj;

    void OnEnable() {
        ani = GetComponent<Animator>();
    }

    void Start() {
        perspectivePivot = transform.GetChild(0).gameObject;
        perspectivePivot.SetActive(false);
        GM = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<Player>().gameObject;
        emotionPivot = transform.GetChild(1).gameObject;
        emotionSR = emotionPivot.transform.GetChild(0).GetComponent<SpriteRenderer>();
        emotionAni = emotionPivot.GetComponent<Animator>();
        playerComp = Player.GetComponent<Player>();
    }

    void Update() {
        if (hello) {
            if (helloDir == FourDirection.上) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * 5);
                if (transform.localPosition.y >= helloDestY) {
                    hello = false;
                    start = true;
                    if (dir == Direction.上下 && status == Status.正方向) {
                        ani.SetTrigger("WalkUp");
                    }
                    else if (dir == Direction.上下 && status == Status.負方向) {
                        ani.SetTrigger("WalkDown");
                    }
                    else if (dir == Direction.左右 && status == Status.負方向) {
                        ani.SetTrigger("WalkLeft");
                    }
                    else {
                        ani.SetTrigger("WalkRight");
                    }
                    perspectivePivot.SetActive(true);
                    angryStatusInstance = StartCoroutine(AngryStatus());
                }
            }
            else if (helloDir == FourDirection.下) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 5);
                if (transform.localPosition.y <= helloDestY) {
                    hello = false;
                    start = true;
                    if (dir == Direction.上下 && status == Status.正方向) {
                        ani.SetTrigger("WalkUp");
                    }
                    else if (dir == Direction.上下 && status == Status.負方向) {
                        ani.SetTrigger("WalkDown");
                    }
                    else if (dir == Direction.左右 && status == Status.負方向) {
                        ani.SetTrigger("WalkLeft");
                    }
                    else {
                        ani.SetTrigger("WalkRight");
                    }
                    perspectivePivot.SetActive(true);
                    angryStatusInstance = StartCoroutine(AngryStatus());
                }
            }
            else if (helloDir == FourDirection.右) {
                transform.localPosition = new Vector2(transform.localPosition.x + speed * 5, transform.localPosition.y);
                if (transform.localPosition.x >= helloDestX) {
                    hello = false;
                    start = true;
                    if (dir == Direction.上下 && status == Status.正方向) {
                        ani.SetTrigger("WalkUp");
                    }
                    else if (dir == Direction.上下 && status == Status.負方向) {
                        ani.SetTrigger("WalkDown");
                    }
                    else if (dir == Direction.左右 && status == Status.負方向) {
                        ani.SetTrigger("WalkLeft");
                    }
                    else {
                        ani.SetTrigger("WalkRight");
                    }
                    perspectivePivot.SetActive(true);
                    angryStatusInstance = StartCoroutine(AngryStatus());
                }
            }
            else {
                transform.localPosition = new Vector2(transform.localPosition.x - speed * 5, transform.localPosition.y);
                if (transform.localPosition.x <= helloDestX) {
                    hello = false;
                    start = true;
                    if (dir == Direction.上下 && status == Status.正方向) {
                        ani.SetTrigger("WalkUp");
                    }
                    else if (dir == Direction.上下 && status == Status.負方向) {
                        ani.SetTrigger("WalkDown");
                    }
                    else if (dir == Direction.左右 && status == Status.負方向) {
                        ani.SetTrigger("WalkLeft");
                    }
                    else {
                        ani.SetTrigger("WalkRight");
                    }
                    perspectivePivot.SetActive(true);
                    angryStatusInstance = StartCoroutine(AngryStatus());
                }
            }
        }
        else if (start) {
            if (dir == Direction.上下) {
                if (status == Status.正方向) {
                    if (transform.localPosition.y <= upOrRightMaxValue - speed && !collide) {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed);
                    }
                    else {
                        status = Status.負方向;
                        ani.SetTrigger("WalkDown");
                        originalAngle = -90.0f;
                        rotationAngle += 180.0f;
                        rotationAngle %= 360.0f;
                        rotationAngle = ModifyAngle(rotationAngle);
                        if (collide) {
                            collide = false;
                            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 5);
                        }
                        else {
                            transform.localPosition = new Vector2(transform.localPosition.x, upOrRightMaxValue - speed + upOrRightMaxValue - transform.localPosition.y);
                        }
                    }
                }
                else {
                    if (transform.localPosition.y >= downOrLeftMaxValue + speed && !collide) {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed);
                    }
                    else {
                        status = Status.正方向;
                        ani.SetTrigger("WalkUp");
                        originalAngle = 90.0f;
                        rotationAngle -= 180.0f;
                        rotationAngle %= 360.0f;
                        rotationAngle = ModifyAngle(rotationAngle);
                        if (collide) {
                            collide = false;
                            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * 5);
                        }
                        else {
                            transform.localPosition = new Vector2(transform.localPosition.x, downOrLeftMaxValue + speed - transform.localPosition.y + downOrLeftMaxValue);
                        }
                    }
                }
            }
            else {
                if (status == Status.正方向) {
                    if (transform.localPosition.x <= upOrRightMaxValue - speed && !collide) {
                        transform.localPosition = new Vector2(transform.localPosition.x + speed, transform.localPosition.y);
                    }
                    else {
                        status = Status.負方向;
                        ani.SetTrigger("WalkLeft");
                        originalAngle = 180.0f;
                        rotationAngle += 180.0f;
                        rotationAngle %= 360.0f;
                        rotationAngle = ModifyAngle(rotationAngle);
                        if (collide) {
                            collide = false;
                            transform.localPosition = new Vector2(transform.localPosition.x - speed * 5, transform.localPosition.y);
                        }
                        else {
                            transform.localPosition = new Vector2(upOrRightMaxValue - speed + upOrRightMaxValue - transform.localPosition.x, transform.localPosition.y);
                        }
                    }
                }
                else {
                    if (transform.localPosition.x >= downOrLeftMaxValue + speed && !collide) {
                        transform.localPosition = new Vector2(transform.localPosition.x - speed, transform.localPosition.y);
                    }
                    else {
                        status = Status.正方向;
                        ani.SetTrigger("WalkRight");
                        originalAngle = 0.0f;
                        rotationAngle += 180.0f;
                        rotationAngle %= 360.0f;
                        rotationAngle = ModifyAngle(rotationAngle);
                        if (collide) {
                            collide = false;
                            transform.localPosition = new Vector2(transform.localPosition.x + speed * 5, transform.localPosition.y);
                        }
                        else {
                            transform.localPosition = new Vector2(downOrLeftMaxValue + speed - transform.localPosition.x + downOrLeftMaxValue, transform.localPosition.y);
                        }
                    }
                }
            }
        }
        else if (bye) {
            if (byeDir == FourDirection.上) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * 5);
                if (transform.localPosition.y >= 6) {
                    bye = false;
                }
            }
            else if (byeDir == FourDirection.下) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 5);
                if (transform.localPosition.y <= -8) {
                    bye = false;
                }
            }
            else if (byeDir == FourDirection.右) {
                transform.localPosition = new Vector2(transform.localPosition.x + speed * 5, transform.localPosition.y);
                if (transform.localPosition.x >= 15) {
                    bye = false;
                }
            }
            else {
                transform.localPosition = new Vector2(transform.localPosition.x - speed * 5, transform.localPosition.y);
                if (transform.localPosition.x <= -15) {
                    bye = false;
                }
            }
        }

        if (start) {
            //perspective rotation
            if (rotationStatus == RotationStatus.逆時針) {
                if (rotationAngle + rotationSpeed > originalAngle + 45) {
                    rotationStatus = RotationStatus.順時針;
                }
            }
            else {
                if (rotationAngle - rotationSpeed < originalAngle - 45) {
                    rotationStatus = RotationStatus.逆時針;
                }
            }

            rotationAngle = (rotationStatus == RotationStatus.逆時針) ? rotationAngle + rotationSpeed : rotationAngle - rotationSpeed;
            rotationAngle = ModifyAngle(rotationAngle);
            perspectivePivot.transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);

            //detect player
            /*float tempDistance = Distance(Player.transform.localPosition, transform.localPosition);
            Vector2 v = new Vector2(perspectiveRadius * Mathf.Cos(rotationAngle * 3.14f / 180), perspectiveRadius * Mathf.Sin(rotationAngle * 3.14f / 180));
            float tempAngle = Mathf.Acos(Dot(GetVector(Player.transform.localPosition, transform.localPosition), v) / tempDistance / perspectiveRadius) * 180 / 3.14f;
            tempAngle = ModifyAngle(tempAngle);
            if (tempDistance <= perspectiveRadius && tempAngle < 45 && tempAngle > -45) {
                perspectivePivot.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            }
            else {
                perspectivePivot.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            }*/

            if (!detectPlayer) {
                perspectivePivot.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = yelloLight;
            }
            else {
                detectPlayer = false;
                perspectivePivot.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = redLight;
                ani.SetTrigger("Stop");
                start = false;
                perspectivePivot.SetActive(false);
                StartCoroutine(AttackPlayer());
            }

            //detect work table
            if (detectWorkTable) {
                detectWorkTable = false;
                start = false;
                perspectivePivot.SetActive(false);
            }

            //release cat
            if(Distance(transform.localPosition, Player.transform.localPosition) < 2 && Input.GetKeyDown(KeyCode.R)) {
                ReleaseCat();
            }
        }
    }

    float Dot(Vector2 v1, Vector2 v2) {
        return v1.x * v2.x + v1.y * v2.y;
    }

    Vector2 GetVector(Vector2 pos1, Vector2 pos2) {
        return new Vector2(pos1.x - pos2.x, pos1.y - pos2.y);
    }

    float Distance(Vector2 pos1, Vector2 pos2) {
        return Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2));
    }

    float ModifyAngle(float angle) {
        if(angle % 360 >= 270 && angle % 360 < 360) {
            angle -= 360.0f;
        }
        return angle;
    }

    public void SetCat(Direction d, float p, float n, float speed, string name, float destX, float destY) {
        dir = d;
        float temp = ((d == Direction.上下) ? destY : destX);
        upOrRightMaxValue = temp + p;
        downOrLeftMaxValue = temp - n;
        this.speed = speed;
        status = (Random.Range(0, 2) == 0) ? Status.正方向 : Status.負方向;
        if(d == Direction.上下) {
            if (status == Status.正方向) {
                originalAngle = 90.0f;
                rotationAngle = 90.0f;
            }
            else {
                originalAngle = -90.0f;
                rotationAngle = -90.0f;
            }
        }
        else {
            if (status == Status.正方向) {
                originalAngle = 0.0f;
                rotationAngle = 0.0f;
            }
            else {
                originalAngle = 180.0f;
                rotationAngle = 180.0f;
            }
        }
        this.name = name;
        helloDestX = destX;
        helloDestY = destY;
        if (Mathf.Abs(helloDestX) >= Mathf.Abs(helloDestY)) {
            helloDir = (helloDestX >= 0) ? FourDirection.左 : FourDirection.右;
            transform.localPosition = (helloDestX >= 0) ? new Vector2(15, destY) : new Vector2(-15, destY);
        }
        else {
            helloDir = (helloDestY >= 0) ? FourDirection.下 : FourDirection.上;
            transform.localPosition = (helloDestY >= 0) ? new Vector2(destX, 6) : new Vector2(destX, -8);
        }
    }

    public void ByeCat() {
        start = false;
        perspectivePivot.SetActive(false);
        if (Mathf.Abs(transform.localPosition.x) >= Mathf.Abs(transform.localPosition.y)) {
            byeDir = (transform.localPosition.x >= 0) ? FourDirection.右 : FourDirection.左;
        }
        else {
            byeDir = (transform.localPosition.y >= 0) ? FourDirection.上 : FourDirection.下;
        }
        bye = true;
        if (byeDir == FourDirection.上) {
            ani.SetTrigger("WalkUp");
        }
        else if (byeDir == FourDirection.下) {
            ani.SetTrigger("WalkDown");
        }
        else if (byeDir == FourDirection.左) {
            ani.SetTrigger("WalkLeft");
        }
        else {
            ani.SetTrigger("WalkRight");
        }
    }

    public void HelloCat() {
        hello = true;
        if(ani == null) {
            ani = GetComponent<Animator>();
        }
        if(helloDir == FourDirection.上) {
            ani.SetTrigger("WalkUp");
        }
        else if(helloDir == FourDirection.下) {
            ani.SetTrigger("WalkDown");
        }
        else if(helloDir == FourDirection.左) {
            ani.SetTrigger("WalkLeft");
        }
        else {
            ani.SetTrigger("WalkRight");
        }
    }

    public bool IsIdle() {
        return start;
    }

    public bool IsExisted() {
        return start || bye || hello || attackPlayer || goBack;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(start) {
            collide = true;
        }
    }

    public void DetectPlayer(GameObject player) {
        if(start && angry) {
            detectPlayer = true;
            detectPlayerObj = player;
        }
    }

    private IEnumerator AttackPlayer() {
        yield return new WaitForSeconds(3.0f);
        ani.SetTrigger("AttackPlayer");
        Vector2 catPos = transform.localPosition;
        Vector2 playerPos = detectPlayerObj.transform.localPosition;
        float distanceX = playerPos.x - catPos.x;
        float distanceY = playerPos.y - catPos.y;
        if(distanceX > 0) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        float deltaX = distanceX / 50.0f;
        float deltaY = distanceY / 50.0f;
        attackPlayer = true;
        
        for (float i = catPos.x, j = catPos.y, k = 0; k < 50; i += deltaX, j += deltaY, k++) {
            transform.localPosition = new Vector2(i, j);
            yield return new WaitForSeconds(1/60);
        }
        checkPlayerDodge = true;
        StartCoroutine(CheckPlayerDodge());
        yield return new WaitForSeconds(2.0f);
        checkPlayerDodge = false;
        if(attackPlayer) {
            ResetRotation();
            goBack = true;
            attackPlayer = false;
            StartCoroutine(GoBack());
        }
    }

    private IEnumerator CheckPlayerDodge() {
        while(true) {
            if (!checkPlayerDodge) break;
            else if(Distance(transform.localPosition, Player.transform.localPosition) < 2) {
                checkPlayerDodge = false;
                ResetRotation();
                goBack = true;
                attackPlayer = false;
                if (playerComp == null) {
                    playerComp = Player.GetComponent<Player>();
                }
                playerComp.PlayerFallDown();
                print("test2");

                StartCoroutine(GoBack());
                break;
            }
            yield return null;
        }
    }

    private void ResetRotation() {
        if(transform.localScale.x < 0) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private IEnumerator GoBack() {
        emotionSR.sprite = loveEmotion;
        StartCoroutine(EmotionShowUpAndDisappear());
        Vector2 catPos = transform.localPosition;
        float distanceX = helloDestX - catPos.x;
        float distanceY = helloDestY - catPos.y;
        float deltaX = distanceX / 50.0f;
        float deltaY = distanceY / 50.0f;
        if(Mathf.Abs(distanceX) >= Mathf.Abs(distanceY)) {
            if(distanceX >= 0) {
                ani.SetTrigger("WalkRight");
            }
            else {
                ani.SetTrigger("WalkLeft");
            }
        }
        else {
            if(distanceY >= 0) {
                ani.SetTrigger("WalkUp");
            }
            else {
                ani.SetTrigger("WalkDown");
            }
        }
        for(float i = catPos.x, j = catPos.y, k =0; k<50; i+=deltaX, j +=deltaY, k++) {
            transform.localPosition = new Vector2(i, j);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        perspectivePivot.SetActive(true);
        perspectivePivot.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = yelloLight;
        if (dir == Direction.上下 && status == Status.正方向) {
            ani.SetTrigger("WalkUp");
        }
        else if (dir == Direction.上下 && status == Status.負方向) {
            ani.SetTrigger("WalkDown");
        }
        else if (dir == Direction.左右 && status == Status.負方向) {
            ani.SetTrigger("WalkLeft");
        }
        else {
            ani.SetTrigger("WalkRight");
        }
        start = true;
        goBack = false;
    }

    public void DetectWorkTable(GameObject workTable) {
        if(workTable == playerComp.GetnowWorkbench()) {
            detectWorkTable = true;
            detectWorkTableObj = workTable;
        }
    }

    private IEnumerator AttackTable() {
        Vector2 catPos = transform.localPosition;
        Vector2 tablePos = detectWorkTableObj.transform.localPosition;
        float distanceX = tablePos.x - catPos.x;
        float distanceY = tablePos.y - catPos.y;
        if (distanceX > 0) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        float deltaX = distanceX / 60.0f;
        float deltaY = distanceY / 60.0f;

        for (float i = catPos.x, j = catPos.y, k = 0; k < 60; i += deltaX, j += deltaY, k++) {
            transform.localPosition = new Vector2(i, j);
            yield return new WaitForSeconds(1 / 60);
        }
        ani.SetTrigger("AttackTable");
        yield return new WaitForSeconds(40/60);
        playerComp.DealnowWorkbenchDamage(50);
        emotionSR.sprite = loveEmotion;
        StartCoroutine(EmotionShowUpAndDisappear());

        goBack = true;
        StartCoroutine(GoBack());
    }

    private IEnumerator AngryStatus() {
        yield return new WaitForSeconds(2 + 1 / 3);
        emotionSR.sprite = angryEmotion;
        emotionAni.SetTrigger("ShowUp");
        yield return new WaitForSeconds(1 / 6);
        emotionAni.SetTrigger("MoreAngry");
        yield return new WaitForSeconds(2.5f);
        angry = true;
    }

    private IEnumerator EmotionShowUpAndDisappear() {
        emotionAni.SetTrigger("ShowUp");
        yield return new WaitForSeconds(0.5f);
        emotionAni.SetTrigger("Disappear");
    }

    public void ReleaseCat() {
        emotionAni.SetTrigger("Disappear");
        StopCoroutine(angryStatusInstance);
        emotionSR.sprite = loveEmotion;
        StartCoroutine(EmotionShowUpAndDisappear());
        angry = false;
        angryStatusInstance = StartCoroutine(AngryStatus());
    }
}

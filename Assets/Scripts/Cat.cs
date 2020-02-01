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
    private Direction dir;
    private float upOrRightMaxValue;    //up and right
    private float downOrLeftMaxValue;    //down and left
    public Status status;
    private float speed;

    private bool start = false;
    private bool bye = false;
    private FourDirection byeDir;

    private bool hello = false;
    private FourDirection helloDir;
    private float helloDestX;
    private float helloDestY;
    private string name;

    void Update() {
        if(hello) {
            if (helloDir == FourDirection.上) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * 2);
                if (transform.localPosition.y >= helloDestY) {
                    hello = false;
                }
            }
            else if (helloDir == FourDirection.下) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 2);
                if (transform.localPosition.y <= helloDestY) {
                    hello = false;
                }
            }
            else if (helloDir == FourDirection.右) {
                transform.localPosition = new Vector2(transform.localPosition.x + speed * 2, transform.localPosition.y);
                if (transform.localPosition.x >= helloDestX) {
                    hello = false;
                }
            }
            else {
                transform.localPosition = new Vector2(transform.localPosition.x - speed * 2, transform.localPosition.y);
                if (transform.localPosition.x <= helloDestX) {
                    hello = false;
                }
            }
        }
        else if(start) {
            if(dir == Direction.上下) {
                if(status == Status.正方向) {
                    if(transform.localPosition.y < upOrRightMaxValue - speed) {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed);
                    }
                    else {
                        status = Status.負方向;
                        transform.localPosition = new Vector2(transform.localPosition.x, upOrRightMaxValue - speed + upOrRightMaxValue - transform.localPosition.y);
                    }
                }
                else {
                    if (transform.localPosition.y > downOrLeftMaxValue + speed) {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed);
                    }
                    else {
                        status = Status.正方向;
                        transform.localPosition = new Vector2(transform.localPosition.x, upOrRightMaxValue + speed - transform.localPosition.y + upOrRightMaxValue);
                    }
                }
            }
            else {
                if (status == Status.正方向) {
                    if (transform.localPosition.x < upOrRightMaxValue - speed) {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.x + speed);
                    }
                    else {
                        status = Status.負方向;
                        transform.localPosition = new Vector2(upOrRightMaxValue - speed + upOrRightMaxValue - transform.localPosition.x, transform.localPosition.y);
                    }
                }
                else {
                    if (transform.localPosition.x > downOrLeftMaxValue + speed) {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.x - speed);
                    }
                    else {
                        status = Status.正方向;
                        transform.localPosition = new Vector2(upOrRightMaxValue + speed - transform.localPosition.x + upOrRightMaxValue, transform.localPosition.y);
                    }
                }
            }
        }
        else if(bye) {
            if(byeDir == FourDirection.上) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * 2);
                if(transform.localPosition.y >= 6) {
                    bye = false;
                }
            }
            else if(byeDir == FourDirection.下) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 2);
                if (transform.localPosition.y <= -6) {
                    bye = false;
                }
            }
            else if(byeDir == FourDirection.右) {
                transform.localPosition = new Vector2(transform.localPosition.x + speed * 2, transform.localPosition.y);
                if (transform.localPosition.x >= 10) {
                    bye = false;
                }
            }
            else {
                transform.localPosition = new Vector2(transform.localPosition.x - speed * 2, transform.localPosition.y);
                if (transform.localPosition.x <= -10) {
                    bye = false;
                }
            }
        }
    }

    public void SetCat(Direction d, float p, float n, float speed, string name) {
        dir = d;
        float temp = ((d == Direction.上下) ? transform.localPosition.y : transform.localPosition.x);
        upOrRightMaxValue = temp + p;
        downOrLeftMaxValue = temp - n;
        this.speed = speed;
        status = (Random.Range(0, 2) == 0) ? Status.正方向 : Status.負方向;
        this.name = name;
    }

    public void ByeCat() {
        start = false;
        bye = true;
        if(Mathf.Abs(transform.localPosition.x) >= Mathf.Abs(transform.localPosition.y)) {
            byeDir = (transform.localPosition.x >= 0) ? FourDirection.右 : FourDirection.左;
        }
        else {
            byeDir = (transform.localPosition.y >= 0) ? FourDirection.上 : FourDirection.下;
        }
    }

    public void HelloCat(float destX, float destY) {
        hello = true;
        helloDestX = destX;
        helloDestY = destY;
        if (Mathf.Abs(destX) >= Mathf.Abs(destY)) {
            helloDir = (destX >= 0) ? FourDirection.左 : FourDirection.右;
        }
        else {
            helloDir = (destY >= 0) ? FourDirection.下 : FourDirection.上;
        }
    }

    public bool IsIdle() {
        return start;
    }

    public bool IsExisted() {
        return start && bye;
    }
}

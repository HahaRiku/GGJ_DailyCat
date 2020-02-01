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

    private GameObject perspectivePivot;
    private float rotationSpeed = 0.1f;
    private float rotationAngle = 0.0f;
    private float originalAngle = 0.0f;
    private RotationStatus rotationStatus = RotationStatus.逆時針;

    void Start() {
        perspectivePivot = transform.GetChild(0).gameObject;
        perspectivePivot.SetActive(false);
    }

    void Update() {
        if(hello) {
            if (helloDir == FourDirection.上) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * 5);
                if (transform.localPosition.y >= helloDestY) {
                    hello = false;
                    start = true;
                    perspectivePivot.SetActive(true);
                }
            }
            else if (helloDir == FourDirection.下) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 5);
                if (transform.localPosition.y <= helloDestY) {
                    hello = false;
                    start = true;
                    perspectivePivot.SetActive(true);
                }
            }
            else if (helloDir == FourDirection.右) {
                transform.localPosition = new Vector2(transform.localPosition.x + speed * 5, transform.localPosition.y);
                if (transform.localPosition.x >= helloDestX) {
                    hello = false;
                    start = true;
                    perspectivePivot.SetActive(true);
                }
            }
            else {
                transform.localPosition = new Vector2(transform.localPosition.x - speed * 5, transform.localPosition.y);
                if (transform.localPosition.x <= helloDestX) {
                    hello = false;
                    start = true;
                    perspectivePivot.SetActive(true);
                }
            }
        }
        else if(start) {
            if(dir == Direction.上下) {
                if(status == Status.正方向) {
                    if(transform.localPosition.y <= upOrRightMaxValue - speed && !collide) {
                        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed);
                    }
                    else {
                        status = Status.負方向;
                        originalAngle = -90.0f;
                        rotationAngle += 180.0f;
                        rotationAngle %= 360.0f;
                        rotationAngle = ModifyAngle(rotationAngle);
                        if(collide) {
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
        else if(bye) {
            if(byeDir == FourDirection.上) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * 5);
                if(transform.localPosition.y >= 6) {
                    bye = false;
                }
            }
            else if(byeDir == FourDirection.下) {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 5);
                if (transform.localPosition.y <= -6) {
                    bye = false;
                }
            }
            else if(byeDir == FourDirection.右) {
                transform.localPosition = new Vector2(transform.localPosition.x + speed * 5, transform.localPosition.y);
                if (transform.localPosition.x >= 10) {
                    bye = false;
                }
            }
            else {
                transform.localPosition = new Vector2(transform.localPosition.x - speed * 5, transform.localPosition.y);
                if (transform.localPosition.x <= -10) {
                    bye = false;
                }
            }
        }


        if(start) {
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
        }
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
            transform.localPosition = (helloDestX >= 0) ? new Vector2(10, destY) : new Vector2(-10, destY);
        }
        else {
            helloDir = (helloDestY >= 0) ? FourDirection.下 : FourDirection.上;
            transform.localPosition = (helloDestY >= 0) ? new Vector2(destX, 6) : new Vector2(destX, -6);
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
    }

    public void HelloCat() {
        hello = true;
    }

    public bool IsIdle() {
        return start;
    }

    public bool IsExisted() {
        return start || bye || hello;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(start) {
            collide = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    public enum Direction {
        上下,
        左右
    }
    private Direction dir;
    private float positiveValue;    //up and right
    private float negativeValue;    //down and left
    private bool start = false;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(start) {
            if(dir == Direction.上下) {

            }
            else {

            }
        }
    }

    public void SetCat(Direction d, float pv, float nv) {
        dir = d;
        pv = positiveValue;
        nv = negativeValue;
    }
    
    public void StartMove() {
        start = true;
    }

    public void ByeCat() {
        start = false;
    }
}

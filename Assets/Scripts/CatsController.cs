using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsController : MonoBehaviour {
    [System.Serializable]
    public enum Type {
        貓進場,
        貓離開,
        等待時間
    }

    [System.Serializable]
    public struct CatData {
        public string name;
        public float destX;
        public float destY;
        public Cat.Direction direction;
        public float 上或右最多多遠;
        public float 下或左最多多遠;
        public float speed;
        public Cat catObj;
    }

    [System.Serializable]
    public struct SequenceElement {
        public Type type;
        public CatData 生成貓;
        public string 離開的貓名字;
        public float 等待時間;
    }

    private List<Cat> CatsPool = new List<Cat>();

    public SequenceElement[] TimeSequence;

    private float startTime;
    private int sequenceIndex;

    // Start is called before the first frame update
    void Start() {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (IsSequenceElementDone()) {
            sequenceIndex++;
            Execute();
        }
    }

    void Execute() {
        if (TimeSequence[sequenceIndex].type == Type.貓進場) {
            bool needToGenerateNewCat = true;
            foreach (Cat c in CatsPool) {
                if(!c.IsExisted()) {
                    needToGenerateNewCat = false;
                    c.SetCat(TimeSequence[sequenceIndex].生成貓.direction, TimeSequence[sequenceIndex].生成貓.上或右最多多遠,
                        TimeSequence[sequenceIndex].生成貓.下或左最多多遠, TimeSequence[sequenceIndex].生成貓.speed,
                        TimeSequence[sequenceIndex].生成貓.name);
                    c.HelloCat(TimeSequence[sequenceIndex].生成貓.destX, TimeSequence[sequenceIndex].生成貓.destY);
                    break;
                } 
            }
            if (needToGenerateNewCat) {

            }
        }
        else if (TimeSequence[sequenceIndex].type == Type.貓離開) {
            
        }
        else {

        }
    }

    bool IsSequenceElementDone() {
        if (TimeSequence[sequenceIndex].type == Type.貓進場) {

        }
        else if (TimeSequence[sequenceIndex].type == Type.貓離開) {

        }
        else {

        }
        return true;
    }

}

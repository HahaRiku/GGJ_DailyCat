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
    public GameObject CatPrefab;

    private float startTime;
    private int sequenceIndex = -1;
    private Cat currentC;

    // Start is called before the first frame update
    void Start() {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (sequenceIndex != TimeSequence.Length - 1 && IsSequenceElementDone()) {
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
                        TimeSequence[sequenceIndex].生成貓.name, TimeSequence[sequenceIndex].生成貓.destX, TimeSequence[sequenceIndex].生成貓.destY);
                    c.HelloCat();
                    CatsPool.Add(c);
                    currentC = c;
                    break;
                } 
            }
            if (needToGenerateNewCat) {
                Cat c;
                c = Instantiate(CatPrefab, new Vector2(10, 6), Quaternion.identity).GetComponent<Cat>();
                c.SetCat(TimeSequence[sequenceIndex].生成貓.direction, TimeSequence[sequenceIndex].生成貓.上或右最多多遠,
                        TimeSequence[sequenceIndex].生成貓.下或左最多多遠, TimeSequence[sequenceIndex].生成貓.speed,
                        TimeSequence[sequenceIndex].生成貓.name, TimeSequence[sequenceIndex].生成貓.destX, TimeSequence[sequenceIndex].生成貓.destY);
                c.HelloCat();
                CatsPool.Add(c);
                currentC = c;
            }
        }
        else if (TimeSequence[sequenceIndex].type == Type.貓離開) {
            foreach(Cat c in CatsPool) {
                if(c.IsIdle() && c.name == TimeSequence[sequenceIndex].離開的貓名字) {
                    c.ByeCat();
                    currentC = c;
                    break;
                }
            }
        }
        else {
            startTime = Time.time;
        }
    }

    bool IsSequenceElementDone() {
        if (currentC == null) return true;
        if (TimeSequence[sequenceIndex].type == Type.貓進場) {
            return currentC.IsIdle();
        }
        else if (TimeSequence[sequenceIndex].type == Type.貓離開) {
            return !currentC.IsExisted();
        }
        else {
            if (Time.time - startTime >= TimeSequence[sequenceIndex].等待時間) {
                return true;
            }
            else return false;
        }
    }

}

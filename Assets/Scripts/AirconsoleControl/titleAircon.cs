using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;

public class titleAircon : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent UpTrig, DownTrig, CheckTrig;
    void Start()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMessage(int from, JToken data)
    {
        string element = (string)data["element"];
        if (element == "Arrow")
        {
            string key = (string)data["data"]["key"];
            bool isPressed = (bool)data["data"]["pressed"];
            if (!isPressed)
                return;
            switch (key)
            {
                case "up":
                    UpTrig.Invoke();
                    break;
                case "down":
                    DownTrig.Invoke();
                    break;
                default:
                    break;
            }
        }
        else if(element == "Repair")
        {
            CheckTrig.Invoke();
        }
    }
}

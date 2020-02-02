using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
public class airconsoleTest : MonoBehaviour
{
    public Dictionary<int, Player_Platformer> players = new Dictionary<int, Player_Platformer>();
    [SerializeField] private AirconsolePlayer playerMove;
    void Start()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onConnect += OnConnect;
    }

    void OnReady(string code)
    {
        //Since people might be coming to the game from the AirConsole store once the game is live, 
        //I have to check for already connected devices here and cannot rely only on the OnConnect event 
        List<int> connectedDevices = AirConsole.instance.GetControllerDeviceIds();
        foreach (int deviceID in connectedDevices)
        {
            AddNewPlayer(deviceID);
        }
    }

    void OnConnect(int device)
    {
        AddNewPlayer(device);
    }

    private void AddNewPlayer(int deviceID)
    {

        if (players.ContainsKey(deviceID))
        {
            return;
        }

        //Instantiate player prefab, store device id + player script in a dictionary
        //GameObject newPlayer = Instantiate(playerPrefab, transform.position, transform.rotation) as GameObject;
        //players.Add(deviceID, newPlayer.GetComponent<Player_Platformer>());
    }
    void OnMessage(int from, JToken data)
    {
        string element = (string)data["element"];
        if (element == "Arrow")
        {
            string key = (string)data["data"]["key"];
            bool isPressed = (bool)data["data"]["pressed"];
            switch (key)
            {
                case "up":
                    playerMove.PlayerDo("Up", isPressed);
                    break;
                case "down":
                    playerMove.PlayerDo("Down", isPressed);
                    break;
                case "right":
                    playerMove.PlayerDo("Right", isPressed);
                    break;
                case "left":
                    playerMove.PlayerDo("Left", isPressed);
                    break;
                default:
                    break;
            }
        }
        if (element == "Repair")
        {
            bool isPressed = (bool)data["data"]["pressed"];
            playerMove.PlayerDo("Repair", isPressed);
        }
        if (element == "Discard")
        {
            bool isPressed = (bool)data["data"]["pressed"];
            playerMove.PlayerDo("Discard", isPressed);
        }
    }
}

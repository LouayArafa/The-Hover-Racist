using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersSetup : MonoBehaviour
{
    [SerializeField] private int playerID;
    [SerializeField] private TextMeshProUGUI PlayerNameTMP;
    [SerializeField] private TextMeshProUGUI PlayerSpeedTMP;
    [SerializeField] private TextMeshProUGUI PlayerPlaceTMP;

    [SerializeField] private VehicleMovement vehicleMovement;
    [SerializeField] private GameObject[] CarsVFX;

    public bool LapIN;
    public bool LapOUT;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        playerID = gameManager.GetPlayerID(); // Assign the player ID from GameManager
        transform.position = gameManager.GetPlayerPosition(playerID).position;
        transform.rotation = gameManager.GetPlayerPosition(playerID).rotation;
        PlayerNameTMP.text = gameManager.GetPlayerName(playerID);

        foreach (GameObject type in CarsVFX)
        {
            if (type.name == gameManager.GetCarType(playerID))
            {
                type.gameObject.SetActive(true);
            }
            else
            {
                type.gameObject.SetActive(false);
            }
        }

        PlayerPlaceTMP.text = "0/" + gameManager.MaxLaps.ToString();

        //last thing
        gameManager.EliBa3douY9awiSa3dou();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSpeedTMP.text = (Mathf.FloorToInt(vehicleMovement.speed) * 5).ToString() + " km/h";

        if (LapIN && LapOUT)
        {
            PlayerPlaceTMP.text = gameManager.GetPlayerLapNum(playerID).ToString() + "/" + gameManager.MaxLaps.ToString();
            LapIN = LapOUT = false;
        }
    }
}

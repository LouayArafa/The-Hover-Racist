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
    [SerializeField] private TextMeshProUGUI PlayerResult;


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

    }

    // Update is called once per frame
    void Update()
    {
        if (LapIN && LapOUT)
        {
            int Laps = gameManager.GetPlayerLapNum(playerID);
            PlayerPlaceTMP.text = Laps.ToString() + "/" + gameManager.MaxLaps.ToString();
            if(Laps == gameManager.MaxLaps)
            { PlayerWon(); }

            LapIN = LapOUT = false;
        }
        PlayerSpeedTMP.text = (Mathf.FloorToInt(vehicleMovement.speed) * 5).ToString() + " km/h";

        if (LapIN && LapOUT)
        {
            LapIN = LapOUT = false;
        }

        if (Input.GetKeyDown("k") && !gameManager.TheGameIsOn)
        {
            gameManager.MaxLaps++;
            PlayerPlaceTMP.text = "0/" + gameManager.MaxLaps.ToString();

        }
        if (Input.GetKeyDown("j") && !gameManager.TheGameIsOn)
        {
            gameManager.MaxLaps--;
            PlayerPlaceTMP.text = "0/" + gameManager.MaxLaps.ToString();

        }
    }
    void PlayerWon()
    {
        PlayerSpeedTMP.enabled = false;
        PlayerNameTMP.enabled = false;
        PlayerPlaceTMP.enabled = false;
        PlayerResult.gameObject.SetActive(true);
        PlayerResult.text = gameManager.GetPlayerResult().ToString() + "#\n Place!";
        vehicleMovement.enabled = false;
    }
}

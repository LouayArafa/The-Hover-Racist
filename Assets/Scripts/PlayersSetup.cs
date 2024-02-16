using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersSetup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerNameTMP;
    [SerializeField] private TextMeshProUGUI PlayerSpeedTMP;
    [SerializeField] private TextMeshProUGUI PlayerPlaceTMP;

    [SerializeField] private VehicleMovement vehicleMovement;
    [SerializeField] private GameObject[] CarsVFX;
    [SerializeField] private int PlayerPlace;

    GameManager gameManager; 
    private void Awake()
    {
        gameManager = GameManager.Instance;
        transform.position = gameManager.GetPlayerPosition().position;
        transform.rotation = gameManager.GetPlayerPosition().rotation;
        PlayerNameTMP.text = gameManager.GetPlayerName();
        
        foreach(GameObject type in CarsVFX)
        {
            if (type.name == gameManager.GetCarType())
            { type.gameObject.SetActive(true); }
            else
            { type.gameObject.SetActive(false); }
        }
        //last thing
        gameManager.EliBa3douY9awiSa3dou();
    }
    // Update is called once per frame
    void Update()
    {
        PlayerSpeedTMP.text = (Mathf.FloorToInt(vehicleMovement.speed)*5).ToString() + " km/h";
        PlayerPlace = gameManager.GetPlayerPlace();
        PlayerPlaceTMP.text = "#" + PlayerPlace.ToString();
    }
}

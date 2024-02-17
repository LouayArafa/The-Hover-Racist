using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] PlayersSetup playersSetup;
    PlayerInputHandler playerInputHandler;
    VehicleMovement vehicleMovement;
    private void Start()
    {
        playersSetup = GetComponentInParent<PlayersSetup>();
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
        vehicleMovement = GetComponentInParent<VehicleMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LapIN"))
        {

            playersSetup.LapIN = true;
        }
        if (other.gameObject.CompareTag("LapOUT") && playersSetup.LapIN == true)
        {
            playersSetup.LapOUT = true;
        }
        if (other.gameObject.CompareTag("Shot"))
        {
            //temp logic
            Debug.Log("player hit");
            vehicleMovement.SlowDown();

        }
        if (other.gameObject.CompareTag("Boost"))
        {
            //temp logic
            Debug.Log("player Boost");
            vehicleMovement.Boost();

        }
    }
}

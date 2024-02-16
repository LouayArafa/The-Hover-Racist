using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] PlayersSetup playersSetup;
    private void Start()
    {
        playersSetup = GetComponentInParent<PlayersSetup>();
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
    }
}

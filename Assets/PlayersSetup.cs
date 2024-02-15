using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersSetup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerName;
    [SerializeField] private GameObject[] CarsVFX; 
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        transform.position = gameManager.GetPlayerPosition().position;
        transform.rotation = gameManager.GetPlayerPosition().rotation;
        PlayerName.text = gameManager.GetPlayerName();
        
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
        
    }
}

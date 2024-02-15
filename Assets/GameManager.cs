using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Singleton instance
public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform[] playersPosition;
    [SerializeField] private string[] PlayersName;
    [SerializeField] private string[] CarsType;

    [SerializeField] private int PlayerIndex;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform GetPlayerPosition()
    {
        Transform tran = playersPosition[PlayerIndex];
        return tran;
    }
    public string GetPlayerName()
    {
        string name = PlayersName[PlayerIndex];
        return name;
    }


    public string GetCarType()
    {
        string Type = CarsType[PlayerIndex];
        return Type;
    }
    public void EliBa3douY9awiSa3dou()
    {
        PlayerIndex++;
    }

}

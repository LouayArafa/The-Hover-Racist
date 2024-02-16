using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int id;
    public Transform playerPosition;
    public string playerName;
    public string carType;
    public int carLapCount;
}

public class GameManager : MonoBehaviour
{
    public bool TheGameIsOn;

    public int MaxLaps;
    [SerializeField] private List<PlayerInfo> playersInfo;
    [SerializeField] private int playerIndex;

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

    public Transform GetPlayerPosition(int playerId)
    {
        PlayerInfo player = FindPlayerById(playerId);
        if (player != null)
        {
            return player.playerPosition;
        }
        return null;
    }

    #region Inisial Functions

    public string GetPlayerName(int playerId)
    {
        PlayerInfo player = FindPlayerById(playerId);
        return player != null ? player.playerName : string.Empty;
    }

    public string GetCarType(int playerId)
    {
        PlayerInfo player = FindPlayerById(playerId);
        return player != null ? player.carType : string.Empty;
    }

    public void EliBa3douY9awiSa3dou()
    {
        playerIndex++;
    }

    #endregion

    #region Real Time Functions

    // Adds 1 to the total of laps a car has
    public int GetPlayerLapNum(int playerId)
    {
        PlayerInfo player = FindPlayerById(playerId);
        if (player != null)
        {
            player.carLapCount++;
            return player.carLapCount;
        }
        return 0;
    }

    public int GetCarSpeed(int playerId)
    {
        // happens in "PlayerSetup"
        return 0;
    }

    private PlayerInfo FindPlayerById(int playerId)
    {
        PlayerInfo foundPlayer = playersInfo.Find(player => player.id == playerId);
        return foundPlayer;
    }

    int playersJoinCount = 0;
    public int GetPlayerID()
    {
        //player info IDs must be 1 2 3 4 in the inspector
        playersJoinCount++;
        if (playersJoinCount < 4)
            return playersJoinCount;
        else
            return 0;
    }
    #endregion
}

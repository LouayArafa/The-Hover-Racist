//This script handles reading inputs from the player and passing it on to the vehicle. We 
//separate the input code from the behaviour code so that we can easily swap controls 
//schemes or even implement and AI "controller". Works together with the VehicleMovement script

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public string gasKey;
    public string brakeKey;
    public string inverseGasKey;
    public string cameraKey;
    public string horizontalAxisName;    //The name of the rudder axis
                                         //We hide these in the inspector because we want 
                                         //them public but we don't want people trying to change them
    [SerializeField] private bool isGasing;
    [SerializeField] private bool isReversing;
        
     public float thruster;            //The current thruster value
     public float rudder;              //The current rudder value
     public bool isBraking;            //The current brake value
     public bool ChangeCam;
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        //If the player presses the Escape key and this is a build (not the editor), exit the game
        if (Input.GetButtonDown("Cancel") && !Application.isEditor)
            Application.Quit();


        isGasing = playerInput.actions[gasKey].IsPressed();
        isReversing = playerInput.actions[inverseGasKey].IsPressed();

        if (isGasing)
        {
            thruster = 1;
        }
        else if (isReversing)
        {
            thruster = -1;
        }
        else
            thruster = 0;
        rudder = playerInput.actions["Move"].ReadValue<Vector2>().x;
        isBraking = playerInput.actions[brakeKey].IsPressed();
        //ChangeCam = playerInput.actions[cameraKey].IsPressed();
    }
}

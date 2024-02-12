//This script handles reading inputs from the player and passing it on to the vehicle. We 
//separate the input code from the behaviour code so that we can easily swap controls 
//schemes or even implement and AI "controller". Works together with the VehicleMovement script

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public string verticalAxisName = "Vertical";        //The name of the thruster axis
    public string horizontalAxisName = "Horizontal";    //The name of the rudder axis
    public string brakingKey = "Brake";                 //The name of the brake button
    //We hide these in the inspector because we want 
    //them public but we don't want people trying to change them
    [HideInInspector] public float thruster;            //The current thruster value
    [HideInInspector] public float rudder;              //The current rudder value
    [HideInInspector] public bool isBraking;            //The current brake value
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

        //If a GameManager exists and the game is not active...


        //Get the values of the thruster, rudder, and brake from the input class
        thruster = playerInput.actions["Move"].ReadValue<Vector2>().y;
        rudder = playerInput.actions["Move"].ReadValue<Vector2>().x;
        isBraking = playerInput.actions["Fire"].WasPerformedThisFrame();
    }
}

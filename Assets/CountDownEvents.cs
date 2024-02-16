using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownEvents : MonoBehaviour
{
    
    public string GameReadyShortcut = "F9"; //change it manually
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StartTheRace()
    {
        GameManager.Instance.TheGameIsOn = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            animator.SetTrigger("Ready");
        }
    }
}

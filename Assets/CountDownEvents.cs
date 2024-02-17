using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownEvents : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bip;
    [SerializeField] private AudioClip biiiiip;
    public string GameReadyShortcut = "F9"; //change it manually
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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

    public void PlayBip()
    {
        audioSource.PlayOneShot(bip);
    }
    public void PlayBiiiip()
    {
        audioSource.PlayOneShot(biiiiip);

    }
}

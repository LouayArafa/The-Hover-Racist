using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log($"hitten object is {col.gameObject.name}");
        if(col.gameObject.tag=="object")
        {

            
        }
    }
}

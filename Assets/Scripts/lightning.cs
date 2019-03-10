using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    private blade _bladeScript;

    private void Start()
    {
        _bladeScript = GameObject.Find("Blade").GetComponent<blade>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (gameObject.name == "lightning")
        {
            if (!other.CompareTag("pidgeon"))
            {
                _bladeScript.KillWoodpeaker(other);   
            }    
        }
        else if (gameObject.name == "snow")
        {
            FreezeObjects();
        }
               
    }
    
    private static void FreezeObjects()
    {
        var woodpeakers = GameObject.FindGameObjectsWithTag("Woodpeakear");
        var pidgeons = GameObject.FindGameObjectsWithTag("pidgeon");
        var unilols = GameObject.FindGameObjectsWithTag("unilol");
        foreach (var pidgeon in pidgeons)
        {
            pidgeon.GetComponent<pidgeon>().Speed = 0;
        }
        foreach (var wp in woodpeakers)
        {
            wp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            wp.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            wp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        foreach (var unilol in unilols)
        {
            unilol.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            unilol.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            unilol.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

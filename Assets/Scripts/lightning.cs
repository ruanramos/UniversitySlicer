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
        if (!other.CompareTag("pidgeon"))
        {
            _bladeScript.KillWoodpeaker(other);   
        }       
    }
}

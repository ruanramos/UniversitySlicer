using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("pidgeon"))
        {
            Destroy(other);    
        }
        
    }
}

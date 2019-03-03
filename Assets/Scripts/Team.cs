using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    private Rigidbody2D _rb;
    private const float StartForce = 15f;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(transform.up*StartForce, ForceMode2D.Impulse);
    }
}

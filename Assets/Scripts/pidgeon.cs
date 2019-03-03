using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class pidgeon : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] public float Speed;
    
    // Start is called before the first frame update
    private void Start()
    {
        Speed = Random.Range(4f, 10f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * Time.deltaTime * Speed);    
        }
        else
        {
            transform.Translate(transform.right * Time.deltaTime * -Speed);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

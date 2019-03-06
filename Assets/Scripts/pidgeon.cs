using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class pidgeon : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] public float Speed;
    public bool IsFalling;
    
    // Start is called before the first frame update
    private void Start()
    {
        Speed = Random.Range(4f, 10f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.rotation.y == 0 && !IsFalling)
        {
            transform.Translate(transform.right * Time.deltaTime * Speed);    
        }
        else if (!(transform.rotation.y == 0) && !IsFalling)
        {
            transform.Translate(transform.right * Time.deltaTime * -Speed);
        }
        else if (IsFalling && transform.rotation.y == 0)
        {
            transform.Translate(new Vector2(1,-0.9f) * Time.deltaTime * Speed);
        }
        else if (IsFalling && !(transform.rotation.y == 0))
        {
            transform.Translate(new Vector2(1,0.9f) * Time.deltaTime * -Speed);
        }
    }

    public void PlayDeathSound()
    {
        GetComponent<AudioSource>().Play();
    }
}

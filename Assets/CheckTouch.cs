using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTouch : MonoBehaviour
{
    private void OnMouseDown()
    {
        SpecialsController.RotateSpecial++;
    }
}

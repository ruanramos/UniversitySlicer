using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsController : MonoBehaviour
{
    public enum Special
    {
        Enel = 1,
        Frozen = 2,
        David = 3
    }

    public static Special SpecialSelected;
    
    // Start is called before the first frame update
    private void Start()
    {
        SpecialSelected = Special.Enel;
    }

    // Update is called once per frame
    private void Update()
    {
        SpecialsInput();

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("HS", 0);
        }
    }

    private static void SpecialsInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpecialSelected = Special.Enel;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpecialSelected = Special.Frozen;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpecialSelected = Special.David;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpecialsController : MonoBehaviour
{
    public enum Special
    {
        Enel = 1,
        Frozen = 2,
        David = 3
    }

    public static Special SpecialSelected;
    public static int RotateSpecial;
    
    // Start is called before the first frame update
    private void Start()
    {
        SpecialSelected = Special.Enel;
        RotateSpecial = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (RotateSpecial > 3)
        {
            RotateSpecial = RotateSpecial % 3;
        }
        
        SpecialsInput();
    }

    private static void SpecialsInput()
    {
        switch (RotateSpecial)
        {
            case 1:
                SpecialSelected = Special.Enel;
                break;
            case 2:
                SpecialSelected = Special.Frozen;
                break;
            case 3:
                SpecialSelected = Special.David;
                break;
        }
    }
}

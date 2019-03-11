using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private int _score;
    private string _highscore;
    
    // Start is called before the first frame update
    private void Start()
    {
        _score = PlayerPrefs.GetInt("Score", 0);
        _highscore = PlayerPrefs.GetString("Highscore", "yes");

        if (_highscore == "yes")
        {
            GameObject.Find("Text").GetComponent<TextMeshProUGUI>().text = "NEW HIGHSCORE \n\nScore: " + _score;
        }
        else
        {
            GameObject.Find("Text").GetComponent<TextMeshProUGUI>().text = "Fergonha está passando mal com sua incompetência \n\nScore: " + _score;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

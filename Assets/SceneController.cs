using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private int _score;
    private string _highscore;

    public Sprite LoseImage;
    public Sprite WinImage;

    public AudioClip LoseSound;
    public AudioClip WinSound;

    public GameObject Text;
    public GameObject Image;
    
    // Start is called before the first frame update
    private void Start()
    {
        
        _highscore = PlayerPrefs.GetString("Highscore", "yes");
        var audioSource = GetComponent<AudioSource>();
        
        if (_highscore == "yes")
        {
            _score = PlayerPrefs.GetInt("Hs", 0);
            Text.GetComponent<TextMeshProUGUI>().text = "Você ganhou e não fez mais que sua obrigação contra a UCW, parabéns pelo novo highscore \n\nScore: " + _score;
            Image.GetComponent<Image>().sprite = WinImage;
            audioSource.clip = WinSound;
        }
        else
        {
            _score = PlayerPrefs.GetInt("Score", 0);
            Text.GetComponent<TextMeshProUGUI>().text = "Fergonha está passando mal com sua incompetência \n\nScore: " + _score + "\nHighscore: " + PlayerPrefs.GetInt("Hs", 0);
            Image.GetComponent<Image>().sprite = LoseImage;
            audioSource.clip = LoseSound;
        }
        audioSource.Play();
    }

    public void Load(string name)
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

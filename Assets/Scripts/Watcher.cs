using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Watcher : MonoBehaviour
{
    private GameObject _redPanel;
    private blade _blade;
    private ParticleSystem _redLightning;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _comboText;

    public static int Score;
    private float _timePlayed;

    public static int ComboCount;

    public static int PidgeonsKilled;

    public GameObject PidgonUi1;
    public GameObject PidgonUi2;
    public GameObject PidgonUi3;

    private Color _myWhite;
    
    //TODO SCORE AND COMBO MECHANIC
    // Start is called before the first frame update
    private void Start()
    {
        _myWhite = new Color(1, 1, 1, 0.5f);
        _redPanel = GameObject.Find("red_panel");
        _blade = GameObject.Find("Blade").GetComponent<blade>();
        _redLightning = GameObject.Find("red_lightning").GetComponent<ParticleSystem>();
        _scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        _comboText = GameObject.Find("Combo").GetComponent<TextMeshProUGUI>();
        ComboCount = 1;
        PidgeonsKilled = 0;
    }


    private void Update()
    {
        _scoreText.text = "Score: " + Score;
        _comboText.text = "X" + ComboCount;

        UpdatePidgeonUi();
    }

    private void UpdatePidgeonUi()
    {
        if (PidgeonsKilled == 0)
        {
            PidgonUi1.GetComponent<Image>().color = _myWhite;
            PidgonUi2.GetComponent<Image>().color = _myWhite;
            PidgonUi3.GetComponent<Image>().color = _myWhite;
        }
        if (PidgeonsKilled > 0)
        {
            PidgonUi1.GetComponent<Image>().color = Color.red;
            PidgonUi2.GetComponent<Image>().color = _myWhite;
            PidgonUi3.GetComponent<Image>().color = _myWhite;
        }

        if (PidgeonsKilled > 1)
        {
            PidgonUi2.GetComponent<Image>().color = Color.red;
            PidgonUi3.GetComponent<Image>().color = _myWhite;
        }
        if (PidgeonsKilled > 2)
        {
            PidgonUi3.GetComponent<Image>().color = Color.red;
            GameOver();
        }
    }

    private static void GameOver()
    {
        Debug.Log(Score);
        Score += (int) Time.time * 500;
        //Score *= (int) (Time.time/10);
        Debug.Log(Score);
        Debug.Log(PlayerPrefs.GetInt("Score", 0));
        if (Score > PlayerPrefs.GetInt("Score", 0))
        {
            PlayerPrefs.SetInt("Score", Score);
            PlayerPrefs.SetString("Highscore", "yes");
            Debug.Log("ENTROU AQUI");
        }
        else
        {
            PlayerPrefs.SetString("Highscore", "no");
        }

        Debug.Log(PlayerPrefs.GetInt("Score", 0));
        Debug.Log(PlayerPrefs.GetString("Highscore", "yes"));
        SceneManager.LoadScene("Final Scene");
    }

    public void GivePenalty()
    {
        _blade.SpecialQuantity = 0;
        StartCoroutine(BlinkRedLight());
        _redLightning.Play();
    }
    
    public IEnumerator BlinkRedLight()
    {
        _redPanel.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        _redPanel.GetComponent<Image>().enabled = false;
    }
    
    public static IEnumerator StopSpawn(float seconds)
    {
        var spawnner = GameObject.Find("Spawnner Woodpeaker");

        if (spawnner == null) yield break;
        spawnner.SetActive(false);
        yield return new WaitForSeconds(seconds);
        spawnner.SetActive(true);
    }
    
    
}

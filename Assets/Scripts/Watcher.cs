using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public static int ComboCount;
    
    //TODO SCORE AND COMBO MECHANIC
    // Start is called before the first frame update
    private void Start()
    {
        _redPanel = GameObject.Find("red_panel");
        _blade = GameObject.Find("Blade").GetComponent<blade>();
        _redLightning = GameObject.Find("red_lightning").GetComponent<ParticleSystem>();
        _scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        _comboText = GameObject.Find("Combo").GetComponent<TextMeshProUGUI>();
        ComboCount = 1;
    }


    private void Update()
    {
        _scoreText.text = "Score: " + Score;
        _comboText.text = "X" + ComboCount;
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

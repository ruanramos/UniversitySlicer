using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEffectsController : MonoBehaviour
{
    private GameObject _enel;
    private GameObject _imageDarkEnel;
    private GameObject _imageDarkFrozen;
    private GameObject _imageDarkDavid;
    private GameObject _imageFillEnel;
    private GameObject _imageFillFrozen;
    private GameObject _imageFillDavid;
    
    private blade _blade;

    public Sprite EnelArmsCrossed;
    public Sprite FrozenArmsCrossed;
    public Sprite SolrakArmsCrossed;

    private void Awake()
    {
        _blade = GameObject.Find("Blade").GetComponent<blade>();
        _imageFillEnel = GameObject.Find("ImageFillEnel");
        _imageFillFrozen = GameObject.Find("ImageFillFrozen");
        _imageDarkEnel = GameObject.Find("ImageEnelDark");
        _imageDarkFrozen = GameObject.Find("ImageFrozenDark");
        _imageFillDavid = GameObject.Find("ImageFillDavid");
        _imageDarkDavid = GameObject.Find("ImageDavidDark");
        
        _imageDarkEnel.SetActive(false);
        _imageFillEnel.SetActive(false);
        _imageDarkFrozen.SetActive(false);
        _imageFillFrozen.SetActive(false);
        _imageFillDavid.SetActive(false);
        _imageDarkDavid.SetActive(false);
    }

    private void Start()
    {
        _enel = GameObject.Find("Enel");
    }
    
    public void UpdateSpecialImage()
    {
        switch (SpecialsController.SpecialSelected)
        {
            case SpecialsController.Special.Enel:
                _imageDarkFrozen.SetActive(false);
                _imageFillFrozen.SetActive(false);
                _imageFillDavid.SetActive(false);
                _imageDarkDavid.SetActive(false);
                _imageDarkEnel.SetActive(true);
                _imageFillEnel.SetActive(true);
                _imageFillEnel.GetComponent<Image>().fillAmount = _blade.SpecialQuantity / blade.SpecialMax;
                break;
            case SpecialsController.Special.Frozen:
                _imageDarkEnel.SetActive(false);
                _imageFillEnel.SetActive(false);
                _imageFillDavid.SetActive(false);
                _imageDarkDavid.SetActive(false);
                _imageDarkFrozen.SetActive(true);
                _imageFillFrozen.SetActive(true);
                _imageFillFrozen.GetComponent<Image>().fillAmount = _blade.SpecialQuantity / blade.SpecialMax;
                break;
            case SpecialsController.Special.David:
                _imageDarkEnel.SetActive(false);
                _imageFillEnel.SetActive(false);
                _imageFillDavid.SetActive(true);
                _imageDarkDavid.SetActive(true);
                _imageDarkFrozen.SetActive(false);
                _imageFillFrozen.SetActive(false);
                _imageFillDavid.GetComponent<Image>().fillAmount = _blade.SpecialQuantity / blade.SpecialMax;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void EnelAppear()
    {
        var image = _enel.GetComponent<Image>();
        image.sprite = EnelArmsCrossed;
        StartCoroutine(Fade(image, 0.1f, 110f));
        StartCoroutine(Watcher.StopSpawn(6f));
    }
    
    public void FrozenAppear()
    {
        var image = _enel.GetComponent<Image>();
        image.sprite = FrozenArmsCrossed;
        StartCoroutine(Fade(image, 0.1f, 110f));
        StartCoroutine(Watcher.StopSpawn(7f));
    }
    public void SolrakAppear()
    {
        var image = _enel.GetComponent<Image>();
        image.sprite = SolrakArmsCrossed;
        StartCoroutine(Fade(image, 0.1f, 110f));
        StartCoroutine(Watcher.StopSpawn(7f));
    }
    
    public void NacibApear(GameObject go)
    {
        var image = GameObject.Find("Nacib").GetComponent<Image>();
        
        UpdateSpecialQuantity(blade.SpecialMax);
        StartCoroutine(TextAppear("Nacib acabou de te fazer uma caridade", 0.1f, 150f));
        StartCoroutine(Fade(image, 0.1f, 150f));
        StartCoroutine(Watcher.StopSpawn(9f));
        DestroyAllObjects();
        Destroy(go);
    }
    
    public void UpdateSpecialQuantity(float quantity)
    {
        _blade.SpecialQuantity += quantity;
    }
    
    private static IEnumerator PanelAppear()
    {
        var panel = GameObject.Find("Panel").GetComponent<Image>();
        panel.color  = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        panel.enabled = true;
        while (panel.color.a <= (float) 100/255)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a + Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        while (panel.color.a >= 0)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a - Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        panel.enabled = false;
    }
    
    public static IEnumerator Fade(Image image, float delay, float limit)
    {
        image.enabled = true;
        while (image.color.a <= limit/255)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime);
            yield return new WaitForSeconds(delay);
        }
        while (image.color.a >= 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1f);
        image.enabled = false;
        var audios = GameObject.Find("Blade").GetComponents<AudioSource>();
        audios[0].Stop();
        audios[1].Stop();
        audios[2].UnPause();
    }
    
    public static IEnumerator TextAppear(string textToShow, float delay, float limit)
    {
        var text = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.text = textToShow;
        text.enabled = true;
        while (text.color.a <= limit/255)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return new WaitForSeconds(delay);
        }
        while (text.color.a >= 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1f);
        text.enabled = false;
    }
    
    public static void DestroyAllObjects()
    {
        var woodpeakers = GameObject.FindGameObjectsWithTag("Woodpeakear");
        var pidgeons = GameObject.FindGameObjectsWithTag("pidgeon");
        var unilols = GameObject.FindGameObjectsWithTag("unilol");
        foreach (var pidgeon in pidgeons)
        {
            Destroy(pidgeon);
        }
        foreach (var wp in woodpeakers)
        {
            Destroy(wp);
        }
        foreach (var unilol in unilols)
        {
            Destroy(unilol);
        }
    }
}

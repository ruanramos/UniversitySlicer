using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Serialization;
using Image = UnityEngine.Experimental.UIElements.Image;

public class blade : MonoBehaviour
{
    private bool _isCutting;

    private Rigidbody2D _rb;
    private Camera _cam;

    public GameObject TrailPrefab;

    private GameObject _currentBladeTrail;

    private CircleCollider2D _bladeCollider;

    private Vector2 _previousPosition;

    private const float MinCuttingVelocity = .0001f;

    public Sprite DeadWoodpeaker;

    private float _enelSpecial;
    private bool _specialReady;

    public Transform SpawnnerSpear;
    public GameObject Spear;
    private const float SpecialMax = 10f;

    private GameObject _imageFill;

    private ParticleSystem _lightning;
    private GameObject _enel;

    public AudioClip EnelTalking;
    public AudioClip Thunder;
    public AudioClip Music;
    //private AudioSource _audioSource;
    
    

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _bladeCollider = GetComponent<CircleCollider2D>();
        _enelSpecial = 0;
        _imageFill = GameObject.Find("ImageFill");
        _lightning = GameObject.Find("lightning").GetComponent<ParticleSystem>();
        _enel = GameObject.Find("Enel");
        //_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }
        if (_isCutting)
        {
            UpdateCut();
        }

        CheckSpecial();
        UpdateSpecialImage();
    }

    private void UpdateSpecialImage()
    {
        _imageFill.GetComponent<UnityEngine.UI.Image>().fillAmount = _enelSpecial / SpecialMax;
    }

    private void CheckSpecial()
    {
        if (_enelSpecial >= SpecialMax)
        {
            _specialReady = true;
        }
        else
        {
            _specialReady = false;
        }

        if (_specialReady)
        {
            GameObject.Find("lightning 2").GetComponent<ParticleSystem>().Play();
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(FireSpecial());
            }
        }
        else
        {
            GameObject.Find("lightning 2").GetComponent<ParticleSystem>().Stop();
        }
    }

    private IEnumerator FireSpecial()
    {
        GameObject.Find("Spawnner Woodpeaker").GetComponent<spawnner>().enabled = false;
        
        _enelSpecial = 0;
        _lightning.Play();
        var audios = GetComponents<AudioSource>();
        audios[0].clip = EnelTalking;
        audios[0].Play();
        audios[1].clip = Thunder;
        audios[1].Play();
        audios[2].Pause();
        StartCoroutine(EnelAppear());
        //StartCoroutine(TextAppear());
        //StartCoroutine(PanelAppear());
        
//        for (var i = 0; i <= 110; i += 10)
//        {
//            var direction = new Vector3(0, 0, -i);
//            var spear = Instantiate(Spear, SpawnnerSpear.position, transform.rotation);
//            spear.transform.Rotate(direction);
//            yield return new WaitForSeconds(0.2f);
//        }
        yield return new WaitForSeconds(3f);
        GameObject.Find("Spawnner Woodpeaker").GetComponent<spawnner>().enabled = true;
    }

    private static IEnumerator TextAppear()
    {
        var text = GameObject.Find("Text").GetComponent<TextMeshPro>();
        //text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.enabled = true;
        while (text.color.a <= 1)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        while (text.color.a >= 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        text.enabled = false;
    }

    private static IEnumerator PanelAppear()
    {
        var panel = GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>();
        //panel.color  = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
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

    private IEnumerator EnelAppear()
    {
        var image = _enel.GetComponent<UnityEngine.UI.Image>();
        image.enabled = true;
        while (image.color.a <= (float) 110/255)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        while (image.color.a >= 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        image.enabled = false;
        var audios = GetComponents<AudioSource>();
        audios[1].Stop();
        audios[2].UnPause();
    }
    
    private void UpdateCut()
    {
        Vector2 newPosition = _cam .ScreenToWorldPoint(Input.mousePosition);
        _rb.position = newPosition;
        var velocity = (newPosition - _previousPosition).magnitude * Time.deltaTime;
        _bladeCollider.enabled = velocity > MinCuttingVelocity;
        _previousPosition = newPosition;
    }

    private void StartCutting()
    {
        _isCutting = true;
        _rb.position = _cam .ScreenToWorldPoint(Input.mousePosition);
        transform.position = _rb.position;
        _currentBladeTrail = Instantiate(TrailPrefab, transform);
        _previousPosition = _cam .ScreenToWorldPoint(Input.mousePosition);
        _bladeCollider.enabled = false;
    }

    private void StopCutting()
    {
        _isCutting = false;
        _currentBladeTrail.transform.SetParent(null);
        Destroy(_currentBladeTrail, 2f);
        _bladeCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Cut(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Cut(other.gameObject);
    }

    private void Cut(GameObject go)
    {
        if (go.CompareTag("Woodpeakear"))
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.sprite = DeadWoodpeaker;
            sr.color = new Color(1, 1, 1, 0.5f);
            go.GetComponent<CapsuleCollider2D>().enabled = false;
            var rb = go.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0.3f;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            _enelSpecial += 1;
            Destroy(go, 2f);
        }
        else if (go.CompareTag("pidgeon"))
        {
            Destroy(go);
        }
    }
}

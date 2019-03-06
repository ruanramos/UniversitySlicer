using System;
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
using Image = UnityEngine.UI.Image;


public class blade : MonoBehaviour
{
    private Watcher _watcher;
    private SpecialEffectsController _specialEffects;
    
    private bool _isCutting;

    private Rigidbody2D _rb;
    private Camera _cam;

    public GameObject TrailPrefab;
    private bool _canCut;

    private GameObject _currentBladeTrail;

    private CircleCollider2D _bladeCollider;

    private Vector2 _previousPosition;

    private const float MinCuttingVelocity = .0001f;

    public Sprite DeadWoodpeaker;

    [FormerlySerializedAs("_enelSpecial")] public float EnelSpecial;
    [FormerlySerializedAs("_specialReady")] public bool SpecialReady;

    public Transform SpawnnerSpear;
    public GameObject Spear;
    public const float SpecialMax = 1f;

    private ParticleSystem _lightning;

    public AudioClip EnelTalking;
    public AudioClip Thunder;
    public AudioClip AngelSound;

    private spawnner _spawnnerScript;

    private void Start()
    {
        GetInitialReferences();
    }

    private void GetInitialReferences()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _bladeCollider = GetComponent<CircleCollider2D>();
        EnelSpecial = 0;
        _lightning = GameObject.Find("lightning").GetComponent<ParticleSystem>();
        _watcher = GameObject.Find("Watcher").GetComponent<Watcher>();
        _spawnnerScript = GameObject.Find("Spawnner Woodpeaker").GetComponent<spawnner>();
        _specialEffects = _watcher.GetComponent<SpecialEffectsController>();

        _canCut = true;
        //_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GetBladeInput();
        if (_isCutting)
        {
            UpdateCut();
        }

        CheckSpecial();
        _specialEffects.UpdateSpecialImage();
    }

    private void GetBladeInput()
    {
        if (Input.GetMouseButtonDown(0) && _canCut)
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0) && _canCut && _isCutting)
        {
            StopCutting();
        }
    }

    private void CheckSpecial()
    {
        SpecialReady = EnelSpecial >= SpecialMax;

        if (SpecialReady)
        {
            GameObject.Find("lightning 2").GetComponent<ParticleSystem>().Play();
            var audios = GetComponents<AudioSource>();
            if (Input.GetMouseButtonDown(1) && GameObject.Find("Nacib").GetComponent<Image>().color.a <= 0 && audios[2].isPlaying)
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
        StopMusicPlaySpecialEffect();
        StartCoroutine(DeactivateBlade());
        _specialEffects.EnelAppear();
        StartCoroutine(SpecialEffectsController.TextAppear("Quando o Enel sair, esse time acaba..." ,0.1f, 90));
        
//        for (var i = 0; i <= 110; i += 10)
//        {
//            var direction = new Vector3(0, 0, -i);
//            var spear = Instantiate(Spear, SpawnnerSpear.position, transform.rotation);
//            spear.transform.Rotate(direction);
//            yield return new WaitForSeconds(0.2f);
//        }
        yield return null;
        //GameObject.Find("Spawnner Woodpeaker").GetComponent<spawnner>().enabled = true;
    }

    private void StopMusicPlaySpecialEffect()
    {
        _spawnnerScript = Resources.Load<spawnner>("Spawnner Woodpeaker").GetComponent<spawnner>();
        _spawnnerScript.enabled = false;
        
        EnelSpecial = 0;
        _lightning.Play();
        var audios = GetComponents<AudioSource>();
        audios[0].clip = EnelTalking;
        audios[0].Play();
        audios[1].clip = Thunder;
        audios[1].Play();
        audios[2].Pause();
    }
    
    private void StopMusicPlayNacibSoundEffect()
    {
        _spawnnerScript = Resources.Load<spawnner>("Spawnner Woodpeaker").GetComponent<spawnner>();
        _spawnnerScript.enabled = false;
        
        EnelSpecial = SpecialMax;
        var audios = GetComponents<AudioSource>();
        audios[0].clip = AngelSound;
        audios[0].Play();
        audios[2].Pause();
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
            KillWoodpeaker(go);
            _specialEffects.UpdateSpecialQuantity(1);
        }
        else if (go.CompareTag("pidgeon"))
        {
            KillPidgeon(go);
            _watcher.GivePenalty();
        }
        else if (go.CompareTag("unilol"))
        {
            StopCutting();
            StopMusicPlayNacibSoundEffect();
            _specialEffects.NacibApear(go);
            StartCoroutine(DeactivateBlade());
        }
    }

    public void KillWoodpeaker(GameObject go)
    {
        Watcher.Score += 100;
        
        var sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = DeadWoodpeaker;
        sr.color = new Color(1, 1, 1, 0.5f);
        go.GetComponent<CapsuleCollider2D>().enabled = false;
        var rb = go.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.3f;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
        go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Destroy(go, 2f);
    }

    private static void KillPidgeon(GameObject go)
    {
        Watcher.Score += 100;
        
        var pidgeonScript = go.GetComponent<pidgeon>();
        pidgeonScript.IsFalling = true;
        go.GetComponent<BoxCollider2D>().enabled = false;
        pidgeonScript.PlayDeathSound();
        Destroy(go, 3f);
    }

    private IEnumerator DeactivateBlade()
    {
        var audios = GetComponents<AudioSource>();
        _canCut = false;
        yield return new WaitUntil(() => !audios[0].isPlaying);
        _canCut = true;
    }
}

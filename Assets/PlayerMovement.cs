using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public float force = 10f;
    public TMP_Text txtScore;
    public TMP_Text txtStage;
    public AudioClip score;
    public AudioClip flap;
    public AudioClip punch;
    private AudioSource _audio;
    public int stage = 1;
    private Animator _anim;
    private ParticleSystem _impulse;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _impulse = GetComponentInChildren<ParticleSystem>();
    }
    
    void Update()
    {
        txtStage.text = "Stage: " + stage.ToString();
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("joystick button 0")) && !isDead)
        {
            _anim.SetTrigger("FlappyWing");
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.AddForce(new Vector3(0,force,0), ForceMode.Impulse);
            _audio.PlayOneShot(flap);
            _impulse.Play();
        }

        if (stage == 1 & Convert.ToInt32(txtScore.text) >= 10 & Convert.ToInt32(txtScore.text) <= 20)
        {
            stage++;
            FindObjectOfType<SpawnObstacles>().spawnTime -= 1f;
            FindObjectOfType<ObstacleMove>().speed += 1f;
            FindObjectOfType<MaterialScroll>().speedTexture += 0.005f;
        }
        else if (stage == 2 & Convert.ToInt32(txtScore.text) >= 21 & Convert.ToInt32(txtScore.text) <= 30)
        {
            stage++;
            FindObjectOfType<SpawnObstacles>().spawnTime -= 1f;
            FindObjectOfType<ObstacleMove>().speed += 1f;
            FindObjectOfType<MaterialScroll>().speedTexture += 0.0025f;
        }
        else if (stage == 3 & Convert.ToInt32(txtScore.text) >= 31 & Convert.ToInt32(txtScore.text) <= 40)
        {
            stage++;
            FindObjectOfType<SpawnObstacles>().spawnTime -= 1f;
            FindObjectOfType<ObstacleMove>().speed += 1f;
            FindObjectOfType<MaterialScroll>().speedTexture += 0.0015f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                _rb.velocity = new Vector3(0, 0, 0);
                _rb.useGravity = false;
                _audio.PlayOneShot(punch);
                isDead = true;
                StartCoroutine(nameof(ReloadScene));
                break;
            case "Ground":
                _audio.PlayOneShot(punch);
                isDead = true;
                StartCoroutine(nameof(ReloadScene));
                break;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PassCollider":
                collision.gameObject.SetActive(false);
                var newScore = Convert.ToInt32(txtScore.text) + 1;
                txtScore.text = newScore.ToString();
                _audio.PlayOneShot(score);
                break;
            case "Coin":
                var newScoreCoin = Convert.ToInt32(txtScore.text) + 5;
                txtScore.text = newScoreCoin.ToString();
                _audio.PlayOneShot(score);
                Destroy(collision.gameObject);
                break;
        }
    }

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

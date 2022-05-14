using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public Transform firepoint;
    public float fireForce = 10;
    [SerializeField] private AudioClip shot_sound;
    AudioSource PlayerAudio;
    GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAudio = GetComponent<AudioSource>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && GameManager.GameActive)
        {
            PlayerAudio.PlayOneShot(shot_sound, 1f);
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firepoint.forward * fireForce, ForceMode.Impulse);
    }

}
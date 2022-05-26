using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioClip shot_sound;

    public float fireForce = 10;
    public int maxAmmo = 25;
    public int crrentAmmo;
    private float reloadTime = 2f;
    public bool isReload = false;

    public Transform firepoint;
    private AudioSource PlayerAudio;
    private GameManager GameManager;

    void Start()
    {
        PlayerAudio = GetComponent<AudioSource>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        crrentAmmo = maxAmmo;
    }

    void Update()
    {
        GameManager.CountAmmo(crrentAmmo);
        if (isReload)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) || crrentAmmo == 0)
        {
            StartCoroutine(ReloadAmmo());
            return;
        }
        if (Input.GetButtonDown("Fire1") && GameManager.GameActive)
        {
            PlayerAudio.PlayOneShot(shot_sound, 1f);
            Shoot();
        }
    }

    IEnumerator ReloadAmmo()
    {
        isReload = true;
        yield return new WaitForSeconds(reloadTime);
        crrentAmmo = maxAmmo;
        isReload = false;
    }

    void Shoot()
    {
        crrentAmmo--;
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firepoint.forward * fireForce, ForceMode.Impulse);
    }

}
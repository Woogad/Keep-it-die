using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float xBound = 11f;
    private float zBound = 6f;
    private Animator anim;
    GameObject MousePos;
    GameManager GameManager;
    AudioSource PlayerAudio;
    Item item;
    [SerializeField] AudioClip bombSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip shiedSound;
    public bool isPlayerAlive;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MousePos = GameObject.Find("Point");
        item = GetComponent<Item>();
        anim = GetComponent<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
        isPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameActive)
        {
            MovePlayer();
            PlayerLimitMove();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
            {
                anim.SetBool("isRun", true);
            }
            else
            {
                anim.SetBool("isRun", false);
            }
        }

    }

    void PlayerLimitMove()
    {
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
    }
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * VerticalInput, Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput, Space.World);
        //* Player angle
        Vector3 relative = transform.InverseTransformPoint(MousePos.transform.position);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        transform.Rotate(0, angle, 0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You hit the" + other.gameObject.name);
            GameManager.GameActive = false;
            isPlayerAlive = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item_bomb"))
        {
            PlayerAudio.PlayOneShot(bombSound);
            Destroy(other.gameObject);
            Debug.Log("bomb");
        }
        if (other.gameObject.CompareTag("Item_heal"))
        {
            PlayerAudio.PlayOneShot(healSound);
            Destroy(other.gameObject);
            Debug.Log("heal");
        }
        if (other.gameObject.CompareTag("Item_shied"))
        {
            PlayerAudio.PlayOneShot(shiedSound);
            Destroy(other.gameObject);
            Debug.Log("shied");
        }
    }
}
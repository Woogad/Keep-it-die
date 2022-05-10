using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float xBound = 11f;
    private float zBound = 6f;
    public bool isGameOver = false;
    private Animator anim;
    GameObject MousePos;
    [SerializeField] AudioClip boom_sound;
    [SerializeField] AudioClip heal_sound;
    [SerializeField] AudioClip shied_sound;
    AudioSource PlayerAudio;
    // Start is called before the first frame update
    void Start()
    {
        MousePos = GameObject.Find("Point");
        anim = GetComponent<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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
            isGameOver = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item_heal"))
        {
            PlayerAudio.PlayOneShot(heal_sound, 1f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Item_shied"))
        {
            PlayerAudio.PlayOneShot(shied_sound, 1f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Item_bomb"))
        {
            PlayerAudio.PlayOneShot(heal_sound, 1f);
            Destroy(other.gameObject);
        }
    }
}
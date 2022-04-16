using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float xBound = 11f;
    private float zBound = 6f;
    public bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        PlayerLimitMove();
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
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Item_shied"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Item_bomb"))
        {
            Destroy(other.gameObject);
        }
    }
}
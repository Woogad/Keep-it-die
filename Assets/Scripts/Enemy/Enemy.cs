using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 100;
    private GameObject player;
    private Animator animator;
    private Vector3 player_pos;
    private Collider m_collider;
    private Rigidbody EnemyRb;

    private bool isAlive = true;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();
        EnemyRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Enemy_Move();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            isAlive = false;
            animator.SetTrigger("isDead");
            Destroy(other.gameObject);
            EnemyRb.constraints = RigidbodyConstraints.FreezeAll;
            m_collider.enabled = false;
            StartCoroutine(Delete_Enemy());
        }

        IEnumerator Delete_Enemy()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }

    private void Enemy_Move()
    {
        if (isAlive)
        {
            //* Enemy angle
            Vector3 relative = transform.InverseTransformPoint(player.transform.position);
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;

            Vector3 LookDirection = (player.gameObject.transform.position - transform.position).normalized;
            transform.Translate(LookDirection * speed * Time.deltaTime, Space.World);
            transform.Rotate(0, angle, 0);
        }

    }
}

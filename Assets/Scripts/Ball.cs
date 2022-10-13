using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float velocity;
    private float timeInstaciated;
    private bool isRolling;
    private Vector2 initialPos;
    private Rigidbody2D rb;
    private int sideToInit = 1;
    public int SideToInit
    {
        get { return sideToInit; }
        set { sideToInit = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
        isRolling = false;
        timeInstaciated = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.State == GameState.PlayState)
        {
            if (Time.time > timeInstaciated + GameManager.Instance.timeToStart && !isRolling)
            {
                rb.AddForce(new Vector2(velocity * sideToInit, velocity * Random.Range(-1f, 1f)));
                isRolling = true;
            }
        }
        else
        {
            timeInstaciated = Time.time;
        }
    }

    public void Restart()
    {
        transform.position = initialPos;
        timeInstaciated = Time.time;
        isRolling = false;
        rb.velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Paddle")
        {
            rb.velocity = Vector2.zero;

            Vector3 direction = transform.position - other.transform.position;
            //float directionY;
            //if (direction.y > -0.3f && direction.y < 0.3f)
            //{
            //    directionY = Random.value > 0.5f ? 0.3f : -0.3f;
            //}
            //else
            //{
            //    directionY = direction.y;
            //}

            rb.AddForce(-other.contacts[0].normal + new Vector2(velocity, velocity * direction.y));
        }
    }
}

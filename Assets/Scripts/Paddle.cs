using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float velocity;
    private Controller controller;
    bool isBottomAndGoingUp;
    bool isTopAndGoingDown;
    float realVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        float upperLimit = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight)).y;
        float bottomLimit = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, 0)).y;
        realVelocity = controller.GetControl() * velocity;
        isBottomAndGoingUp = rb.position.y + 1 < upperLimit && realVelocity > 0;
        isTopAndGoingDown = rb.position.y - 1 > bottomLimit && realVelocity < 0;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.State == GameState.PlayState && (isTopAndGoingDown || isBottomAndGoingUp))
        {
            rb.velocity = new Vector2(0f, realVelocity);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}

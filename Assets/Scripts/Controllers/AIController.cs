using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    private Transform _ballTransform;

    void Update()
    {
        if (_ballTransform == null && GameObject.Find("Ball")) _ballTransform = GameObject.Find("Ball").transform;
    }

    public override float GetControl()
    {
        if (_ballTransform == null) return 0f;
        
        if(_ballTransform.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            float ballVerticalDirection = (transform.position - _ballTransform.position).y;
            if (ballVerticalDirection > 0.5f) return -1;
            else if (ballVerticalDirection < -0.5f) return 1;
            else return 0;
        }

        return 0;
    }
}

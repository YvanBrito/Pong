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
        
        float ballVerticalDirection = (transform.position - _ballTransform.position).y;
        return ballVerticalDirection > 0 ? -1 : 1;
    }
}

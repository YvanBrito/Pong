using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : Controller
{
    public override float GetControl()
    {
        return Input.GetAxisRaw(transform.name + "Vertical");
    }
}

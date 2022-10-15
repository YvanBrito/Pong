using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : Controller
{
    [SerializeField] private string playerName;
    public override float GetControl()
    {
        return Input.GetAxisRaw(playerName + "Vertical");
    }
}

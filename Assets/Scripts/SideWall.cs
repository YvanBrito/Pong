using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Ball")
        {
            if (transform.name == "WallLeft")
            {
                ScoreManager.Instance.AddSecondPlayerPoint();
            }
            else if (transform.name == "WallRight")
            {
                ScoreManager.Instance.AddFirstPlayerPoint();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAnim : MonoBehaviour
{
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("Run");
    }
}

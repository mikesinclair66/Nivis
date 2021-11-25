using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablerAnim : MonoBehaviour
{
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("Run");
    }
}

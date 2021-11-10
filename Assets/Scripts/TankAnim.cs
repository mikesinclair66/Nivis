using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnim : MonoBehaviour
{
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("Walk");
    }

    public void PlayDeathAnim()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("Death");
    }
}

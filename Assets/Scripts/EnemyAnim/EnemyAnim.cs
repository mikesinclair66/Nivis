using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("Run");
    }
}

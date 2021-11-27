using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public MeshRenderer mRend;
    public Turret turret;
    public Pulsor pulsor;
    public GameObject forcefieldRange;

    // Start is called before the first frame update
    void Start()
    {
        turret = GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateRangeIndicator()
    {
        mRend.enabled = true;
        if (turret != null)
        {
            if (turret.turretName == "turret")
            {
                forcefieldRange.transform.localScale = new Vector3(turret.range * 5, 5f, turret.range * 5);
            }
        }
        if (pulsor != null)
        {
            if (pulsor.turretName == "pulsor")
            {
                forcefieldRange.transform.localScale = new Vector3(pulsor.pulsorRange*2, 1f, pulsor.pulsorRange*2);
            }
        }

    }
    public void deactivateRangeIndicator()
    {
        mRend.enabled = false;
    }
}

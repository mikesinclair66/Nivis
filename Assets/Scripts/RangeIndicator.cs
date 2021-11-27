using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public MeshRenderer mRend;
    public Turret turret;
    public Pulsor pulsor;
    public GameObject forcefieldRange;

    void Start()
    {
        turret = GetComponent<Turret>();
    }

    public void ghostRangeIndicator(GameObject obj, int turretToBuildType)
    {
        if (turretToBuildType == 0 && turretToBuildType == 1)
        {
            forcefieldRange.transform.localScale = new Vector3(30f, 0.1f, 30f);
        }
        if (turretToBuildType == 2)
        {
            forcefieldRange.transform.localScale = new Vector3(20f, 0.1f, 20f);
        }
    }

    public void activateRangeIndicator()
    {
        if (turret != null)
        {
            if (turret.turretName == "turret")
            {
                forcefieldRange.transform.localScale = new Vector3(turret.range * 2, 0.1f, turret.range * 2);
            }
            if (turret.turretName == "missile")
            {
                forcefieldRange.transform.localScale = new Vector3(turret.range * 2, 0.1f, turret.range * 2);
            }
            if (turret.turretName == "sniper")
            {
                forcefieldRange.transform.localScale = new Vector3(0, 0, 0);
            }
        }
        if (pulsor != null)
        {
            if (pulsor.turretName == "pulsor")
            {
                forcefieldRange.transform.localScale = new Vector3(pulsor.range * 2, 0.1f, pulsor.range * 2);
            }
        }
        mRend.enabled = true;
    }
    public void deactivateRangeIndicator()
    {
        mRend.enabled = false;
    }
}

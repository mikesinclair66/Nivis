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

    // Activates the indicator of the range of the turret in a ghost for the player to see before placing
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

    // Activates the range indicator based on range of turret chosen
    public void activateRangeIndicator()
    {
        if (turret != null)
        {
            switch (turret.turretName)
            {
                case "turret":
                    forcefieldRange.transform.localScale = new Vector3(turret.range * 2, 0.1f, turret.range * 2);
                    break;
                case "laser":
                    forcefieldRange.transform.localScale = new Vector3(turret.range * 2, 0.1f, turret.range * 2);
                    break;
                case "sniper":
                    forcefieldRange.transform.localScale = new Vector3(0, 0, 0);
                    break;
                case "missile":
                    forcefieldRange.transform.localScale = new Vector3(turret.range * 2, 0.1f, turret.range * 2);
                    break;
                default:
                    forcefieldRange.transform.localScale = new Vector3(0, 0, 0);
                    break;
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

        // Deactivate the range indicator
    public void deactivateRangeIndicator()
    {
        mRend.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public Enemy enemy;
    public Debuff debuff;
    ArrayList activeDebuffs = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        debuff = GetComponent<Debuff>();
    }

    // Update is called once per frame
    void Update()
    {
        activeDebuffs = debuff.returnActiveDebuffs();
    }
}
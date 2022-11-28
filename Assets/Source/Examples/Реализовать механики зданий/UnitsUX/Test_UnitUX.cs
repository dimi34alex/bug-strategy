using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UnitUX : MonoBehaviour, IDamageApplicator
{
    public float Damage => damage;
    [SerializeField] private float damage = 10;
    [SerializeField] private float pauseBetweenAttack = 0;
    [SerializeField] private MovingUnit TestAlonBee;
    private bool takeDamage = false;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (takeDamage && timer >= pauseBetweenAttack)
        {
            TestAlonBee.TakeDamage(this);
            timer = 0;
        }
    }
    
    public void _DamegedBee()
    {
        takeDamage = true;
    }
    
    public void _UseFirstAbility()
    {
        TestAlonBee.UseFirstAbility();
    }   
    public void _UseSecondAbility()
    {
        TestAlonBee.UseSecondAbility();
    }
    
}

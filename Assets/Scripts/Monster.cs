using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    [SerializeField] private int _reward;

    private CapsuleCollider _bodyCollider;
    public int Reward { get { return _reward; } }

    private void Start()
    {

        _bodyCollider = gameObject.AddComponent<CapsuleCollider>();
        _bodyCollider.isTrigger = true;
        _bodyCollider.center = new Vector3(0, 1.58f, 0);
        _bodyCollider.radius = 2.1f;
        _bodyCollider.height = 6.89f;
        _bodyCollider.direction = 2;

        healthBar = GetHealthBar("BarFilledMonster");
        
        SetMaxHealthBar();

        StartCoroutine(Regeneration(0.5f));
    }

    public override void TakeDamage(int damage)
    {
        if (!Dead && Health <= 0)
        {
            GameController._monsterDied.Invoke();
            Health = 0;
            Dead = true;
        }
        else
            Health -= damage;

        SetHealthBar(Health);
    }
}

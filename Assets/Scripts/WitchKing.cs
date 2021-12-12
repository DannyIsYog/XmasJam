using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchKing : MonoBehaviour
{

    private Animator _animator;
    private Entity _entity;

    //private Transform _player;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _entity = GetComponent<Entity>();
        _entity.onEnemyAttack += OnPlayerAttack;
    }

    void OnPlayerAttack(int strength)
    {
        if (_entity.TakeDamage(strength))
        {
            Destroy(gameObject);
        }
    }
}

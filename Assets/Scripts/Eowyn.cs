using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eowyn : MonoBehaviour
{
    private Animator _animator;
    private bool _defending = false;

    private Entity _entity;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _entity = GetComponent<Entity>();
        _entity.onEnemyAttack += OnEnemyAttack;
    }

    void Update()
    {
        if (_defending && (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Z)))
            Shield();
        else if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.X))
            Attack();
        else if (!_defending && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Z)))
            Shield();

        _entity.SetMovement(Input.GetAxisRaw("Horizontal"));
    }

    void Attack()
    {
        _defending = false;
        _animator.SetBool("Shield", _defending);
        _animator.SetTrigger("Attack");
        _entity.Attack();
    }

    void Shield()
    {
        _defending = !_defending;
        _animator.SetBool("Shield", _defending);
    }

    void OnEnemyAttack(float strenght)
    {
        if (!_defending)
            _entity.TakeDamage(strenght);
    }
}

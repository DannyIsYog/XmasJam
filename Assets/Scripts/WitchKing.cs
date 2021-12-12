using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchKing : MonoBehaviour
{
    private bool _part1 = true;
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
            if (!_part1)
            {
                Destroy(gameObject);
                EndScreen.win = true;
                SceneManager.LoadScene("EndScreen");
            }
            else
            {
                _entity.RestoreHealth(true);
                _part1 = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Eowyn>().DestroyShield();
            }
        }
    }
}

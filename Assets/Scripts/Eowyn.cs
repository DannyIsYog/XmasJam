using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Eowyn : MonoBehaviour
{
    private Animator _animator;
    private bool _defending = false;
    private bool _hasShield = true;
    [SerializeField] private float _shieldStrength = 0.6f;

    private Entity _entity;
    private Rigidbody2D _rb;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _entity = GetComponent<Entity>();
        _entity.onEnemyAttack += OnEnemyAttack;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_defending && !_hasShield)
            Shield();

        if (_hasShield && _defending && (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Z)))
            Shield();
        else if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.X))
            Attack();
        else if (_hasShield && !_defending && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Z)))
            Shield();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            if (_rb.velocity.y == 0)
                _rb.AddForce(Vector2.up * 1000f);

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

    void OnEnemyAttack(int strength)
    {
        bool dies = false;
        if (!_defending || !_hasShield)
            dies = _entity.TakeDamage(strength);
        else
            dies = _entity.TakeDamage((int)Random.Range(0f, _shieldStrength * strength));
        if (dies)
        {
            Destroy(gameObject);
            EndScreen.win = false;
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void DestroyShield()
    {
        _hasShield = false;
        _entity.RestoreHealth(false);
    }
}

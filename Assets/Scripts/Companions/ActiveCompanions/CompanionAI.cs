using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAI : MonoBehaviour
{
    // Приватные переменные

    private Transform _companionPos;
    private GameObject _enemies;
    private GameObject _attackTarget;
    private Vector3 _moveTarget;
    private Rigidbody2D _rb;
    private bool isAttack = false;
    private bool _canAttack;
    private Animator _anim;

    // Приватные переменные что задаются в едиторе

    [SerializeField] private float lookDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float patrolDistance; // It will be just private

    // Публичные свойства к переменным
    public bool IsAttack { get { return isAttack; } }
    public float Damage;
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    // Основные методы

    void Start()
    {
        SetStartValues();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttack)
        {
            _moveTarget = RandomPosition();
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            _canAttack = false;
            isAttack = false;
            Invoke("CanAttack", 3);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isAttack)
        {
            _moveTarget = RandomPosition();
        }
    }

    // Методы-сервисы, что выполняют действия, в которых разбираться необязательно :)
    // Тут ещё разделение будет, но абсолютно всё что дальше - методы-сервисы, просто разных видов

    // Методы-сервисы старта игры

    private void SetStartValues()
    {
        _companionPos = gameObject.GetComponent<Transform>();
        _enemies = GameObject.FindGameObjectWithTag("Enemies");
        _moveTarget = RandomPosition();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _rb.isKinematic = false;
        _rb.drag = 10000000; 
        _rb.velocity = new Vector2(0, 0);
        _canAttack = true;
        _anim = GetComponent<Animator>();
    }

    // Методы-сервисы движения врага

    private void Move()
    {
        SetAttack();
        MoveEnemy();
        LookAt(_moveTarget);
        if (!isAttack && transform.position == _moveTarget)
        {
            _moveTarget = RandomPosition();
        }
    }

    private void SetAttack()
    {
        if (CheckAttack() && _canAttack)
        {
            _moveTarget = _attackTarget.transform.position;
            isAttack = true;
            return;
        }
        isAttack = false;
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(_companionPos.position, _moveTarget, moveSpeed * Time.deltaTime);
    }

    private void LookAt(Vector3 target)
    {
        if (target.x > _companionPos.position.x)
        {
            _companionPos.rotation = new Quaternion(_companionPos.rotation.x, 0f, _companionPos.rotation.z, _companionPos.rotation.w);
        }
        else
        {
            _companionPos.rotation = new Quaternion(_companionPos.rotation.x, 180f, _companionPos.rotation.z, _companionPos.rotation.w);
        }
    }

    private Vector3 RandomPosition()
    {
        var x = UnityEngine.Random.Range(-16, 16);
        var y = UnityEngine.Random.Range(-5, 7);
        var z = _companionPos.position.z;
        return new Vector3(x, y, z);
    }

    private bool CheckAttack()
    {
        ChooseEnemy();
        if (_attackTarget != null)
        {
            var distance = Vector2.Distance(_attackTarget.transform.position, _companionPos.position);
            if (isAttack)
            {
                if (distance < lookDistance)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (!_canAttack)
                {
                    return false;
                }
                else
                {
                    if (distance < lookDistance)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        else
        {
            return false;
        }

    }
    private void ChooseEnemy()
    {
        if (_enemies.transform.childCount != 0)
        {
            var bestEnemy = _enemies.transform.GetChild(0);
            for (int i = 0; i < _enemies.transform.childCount; i++)
            {
                if (Vector2.Distance(_enemies.transform.GetChild(i).transform.position, _companionPos.position) <
                    Vector2.Distance(bestEnemy.transform.position, _companionPos.position))
                {
                    bestEnemy = _enemies.transform.GetChild(i);
                }
            }
            _attackTarget = bestEnemy.gameObject;
        }
        else
        {
            _attackTarget = null;
        }
        
    }

    private void CanAttack()
    {
        _canAttack = true;
    }

    private void Death()
    {
        _anim.SetBool(IsDead, true);
        Destroy(gameObject, 1f);
    }

}

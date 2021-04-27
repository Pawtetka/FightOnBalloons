using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Приватные переменные

    private Transform _enemyPos;
    private GameObject _player;
    private GameObject _attackTarget;
    private Vector3 _moveTarget;
    private Rigidbody2D _rb;
    private bool isAttack = false;
    private State _state;
    private Animator _anim;
    private bool isShoot = false;

    // Приватные переменные что задаются в едиторе

    [SerializeField] private float lookDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float patrolDistance; // It will be just private
    [SerializeField] private GameObject balloons;
    [SerializeField] private float inflatingTime;
    [SerializeField] private float shootAngle;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private List<GameObject> castObjects;
    [SerializeField] private string attackAudio;
    private static readonly int Cast = Animator.StringToHash("Cast");
    private static readonly int BackToIdle = Animator.StringToHash("BackToIdle");
    private static readonly int Attack = Animator.StringToHash("Attack");

    // Публичные свойства к переменным

    public enum State { Move, Falling, Inflating, Death }
    public State state { get { return _state; } set { _state = value; }  }
    public bool IsAttack { get { return isAttack; } }

    // Основные методы

    void Start()
    {
        _anim = GetComponent<Animator>();
        SetStartValues();
        FallingState();
        //Invoke("StartMove", inflatingTime);
    }

    private void FixedUpdate()
    {
        if (_state == State.Move)
        {
            Move();
            Shoot();
        }
        if (state == State.Falling && groundCheck.OnGround)
        {
            _state = State.Inflating;
            _rb.velocity = new Vector2(0, 0);
            Invoke("StartMove", inflatingTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttack)
        {
            _moveTarget = RandomPosition();
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
        _enemyPos = gameObject.GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _attackTarget = GameObject.FindGameObjectWithTag("PlayerBalloons");
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _rb.isKinematic = false;
        _rb.drag = 10000000;
        _rb.velocity = new Vector2(0,0);
        _state = State.Falling;
    }


    // Методы-сервисы переключения состояний врага

    private void StartMove()
    {
        if (state != State.Death)
        {
            for (int i = 0; i < balloons.transform.childCount; i++)
            {
                balloons.transform.GetChild(i).gameObject.SetActive(true);
            }
            _state = State.Move;
            _rb.gravityScale = 0f;
            _rb.drag = 10000000;
        }
    }

    private void FallingState()
    {
        if (!CheckBalloons(balloons))
        {
            _state = State.Falling;
            _rb.gravityScale = 0.65f;
            _rb.drag = 0;
        }
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
        FallingState();
    }

    private void SetAttack()
    {
        if (CheckAttack() && castObjects.Count == 0)
        {
            _moveTarget = _attackTarget.transform.position;
            isAttack = true;
            return;
        }
        isAttack = false;
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(_enemyPos.position, _moveTarget, moveSpeed * Time.deltaTime);
    }

    private void LookAt(Vector3 target)
    {
        if (target.x > _enemyPos.position.x)
        {
            _enemyPos.rotation = new Quaternion(_enemyPos.rotation.x, 0f, _enemyPos.rotation.z, _enemyPos.rotation.w);
        }
        else
        {
            _enemyPos.rotation = new Quaternion(_enemyPos.rotation.x, 180f, _enemyPos.rotation.z, _enemyPos.rotation.w);
        }
    }

    private Vector3 RandomPosition()
    {
        float x = UnityEngine.Random.Range(-16, 16);
        float y = UnityEngine.Random.Range(-5, 7);
        float z = _enemyPos.position.z;
        return new Vector3(x, y, z);
    }

    // Методы-сервисы стрельбы

    private void Shoot()
    {
        var distance = Vector2.Distance(_attackTarget.transform.position, _enemyPos.position);
        if ((_player.transform.position.y < _enemyPos.position.y + shootAngle) &&
            (_player.transform.position.y > _enemyPos.position.y - shootAngle) &&
            distance < lookDistance && CheckBalloons(_attackTarget))
        {
            LookAt(_player.transform.position);
            if (isShoot || castObjects.Count == 0) return;
            isShoot = true;
            _anim.SetTrigger(Attack);
            StartCoroutine("CastCreate", castObjects[0]);
        }
    }

    private IEnumerator CastCreate(GameObject castObject)
    {
        if (attackAudio != null && FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play(attackAudio);
        }
        yield return new WaitForSeconds(0.5f);
        var newCast = Instantiate(castObject, castObject.transform.position,
            castObject.transform.rotation);
        newCast.GetComponent<Bullet>().ParentTag = gameObject.transform.tag;
        newCast.SetActive(true);
        Destroy(newCast, 1.1f);
        _anim.SetTrigger(BackToIdle);
        isShoot = false;
    }

    // Методы-сервисы для проверки той или другой штуки

    private bool CheckBalloons(GameObject ball)
    {
        for(int i = 0; i < ball.transform.childCount; i++)
        {
            if (ball.transform.GetChild(i).gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckAttack()
    {
        var distance = Vector2.Distance(_attackTarget.transform.position, _enemyPos.position);
        var canNewEnemyAttack = transform.parent.gameObject.GetComponent<EnemyMain>().CanNewEnemyAttack;
        if (isAttack)
        {
            if (distance < lookDistance && CheckBalloons(_attackTarget))
            {
                return true;
            }
            return false;
        }
        else
        {
            if (!canNewEnemyAttack)
            {
                return false;
            }
            else
            {
                if (distance < lookDistance && CheckBalloons(_attackTarget))
                {
                    return true;
                }
                return false;
            }
        }
        
    }


}

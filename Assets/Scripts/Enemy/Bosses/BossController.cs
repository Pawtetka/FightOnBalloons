using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : MonoBehaviour
{
    protected Animator _animator;
    protected static readonly int Cast = Animator.StringToHash("Cast");
    public List<GameObject> castAnims;
    protected static readonly int Casted = Animator.StringToHash("Casted");
    protected static readonly int SmallCast = Animator.StringToHash("SmallCast");
    protected static readonly int BackToIdle = Animator.StringToHash("BackToIdle");
    public float health;
    public enum BossState { Move, Cast, SmallCast, Stay, Death}
    protected BossState bossState;
    private bool isAlive = true;
    private SpriteRenderer _spriteRenderer;
    private Color32 _startColor;
    private static readonly int Death = Animator.StringToHash("Death");
    [SerializeField] private string deathAudio;
    [SerializeField] protected List<string> audioEffects;

    private void Start()
    {
        StartValues();
    }

    private void Update()
    {
        UpdateCode();
    }
    public virtual void StartValues()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _animator = GetComponent<Animator>();
        var rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
    }

    public virtual void UpdateCode()
    {
        BossLife();
        if (health <= 0 && isAlive)
        {
            _animator.SetTrigger(Death);
            isAlive = false;
            bossState = BossState.Death;
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play(deathAudio);
            }
            Destroy(gameObject, 1.5f);
        }
    }

    private void BossCast()
    {
        _animator.SetTrigger(Cast);
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play(audioEffects[0]);
        }
        StartCoroutine("CastCreate", castAnims[0]);
    }

    private void BossSmallCast()
    {
        _animator.SetTrigger(SmallCast);
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play(audioEffects[1]);
        }
        StartCoroutine("SmallCastCreate", castAnims[1]);
    }

    public virtual IEnumerator CastCreate(GameObject lightning)
    {
        yield return new WaitForSeconds(0.5f);
        var newLightning = Instantiate(lightning, lightning.transform.position,
            lightning.transform.rotation);
        newLightning.SetActive(true);
        Destroy(newLightning, 1.1f);
        _animator.SetTrigger(BackToIdle);
        bossState = BossState.Move;
    }
     public virtual IEnumerator SmallCastCreate(GameObject lightning)
    {
        yield return new WaitForSeconds(0.5f);
        var newLightning = Instantiate(lightning, lightning.transform.position,
            lightning.transform.rotation);
        newLightning.SetActive(true);
        Destroy(newLightning, 1.1f);
        _animator.SetTrigger(BackToIdle);
        bossState = BossState.Move;
    }

    public virtual void BossLife()
    {
        if (bossState == BossState.Move)
        {
            Movement();
        }
        else if (bossState == BossState.Cast || Input.GetKeyDown(KeyCode.Space))
        {
            BossCast();
            bossState = BossState.Stay;
        }
        else if (bossState == BossState.SmallCast || Input.GetKeyDown(KeyCode.R))
        {
            BossSmallCast();
            bossState = BossState.Stay;
        }
    }
    public virtual void Movement() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && other.GetComponent<Bullet>().ParentTag != gameObject.transform.tag)
        {
            if (bossState == BossState.Move)
            {
                var damage = Convert.ToInt32(other.gameObject.GetComponent<Bullet>().BulletDamage);
                health -= damage;
                StartBlood();
                Invoke("EndBlood", 0.15f);
            }
            Destroy(other.gameObject, 0.01f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnterCode(collision);
    }

    public virtual void OnCollisionEnterCode(Collision2D collision)
    {
        if (collision.gameObject.tag == "Companion" && bossState != BossState.Stay)
        {
            StartBlood();
            Invoke("EndBlood", 0.15f);
            health -= collision.gameObject.GetComponent<CompanionAI>().Damage;
        }
    }

    private void StartBlood()
    {
        _spriteRenderer.color = Color.red;
    }

    private void EndBlood()
    {
        _spriteRenderer.color = _startColor;
    }
}

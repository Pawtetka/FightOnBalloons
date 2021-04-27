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
    public enum BossState { Move, Cast, SmallCast, Stay }
    protected BossState bossState;
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
        _animator = GetComponent<Animator>();
    }

    public virtual void UpdateCode()
    {
        BossLife();
        if (health <= 0)
        {
            //_animator.SetBool();
            Destroy(gameObject, 1);
        }
    }

    private void BossCast()
    {
        
        _animator.SetTrigger(Cast);
        StartCoroutine("CastCreate", castAnims[0]);
    }

    private void BossSmallCast()
    {
        _animator.SetTrigger(SmallCast);
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
            }
            Destroy(other.gameObject, 0.01f);
        }
    }
}

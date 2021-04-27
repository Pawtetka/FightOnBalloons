using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : BossController
{
    private Vector3 _moveTarget;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float attackAngle;
    private GameObject _player;
    private bool canAttack = true;
    public override void StartValues()
    {
        base.StartValues();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void UpdateCode()
    {
        base.UpdateCode();
        if (Math.Abs(gameObject.transform.position.y - _player.transform.position.y) <= attackAngle && canAttack)
        {
            if (bossState == BossState.Move)
            {
                LookAt(_player.transform.position);
                canAttack = false;
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                bossState = BossState.Cast;
            }
        }
    }
    public override IEnumerator CastCreate(GameObject castObject)
    {
        yield return new WaitForSeconds(0.5f);
        if (bossState != BossState.Death)
        {
            castObject.SetActive(true);
            for (int i = 0; i < castObject.transform.childCount; i++)
            {
                yield return new WaitForSeconds(0.05f);
                var obj = castObject.transform.GetChild(i).gameObject;
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
            castObject.SetActive(false);
            for (int i = 0; i < castObject.transform.childCount; i++)
            {
                var obj = castObject.transform.GetChild(i).gameObject;
                obj.SetActive(false);
            }
            _animator.SetTrigger(BackToIdle);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            bossState = BossState.Move;
            Invoke("CanAttack", 3f);
        }

    }

    private void CanAttack()
    {
        canAttack = true;
    }

    private Vector3 RandomPosition(Transform transform)
    {
        var x = UnityEngine.Random.Range(-15, 15);
        var y = UnityEngine.Random.Range(-5, 5);
        var z = transform.position.z;
        return new Vector3(x, y, z);
    }
    public override void Movement()
    {
        MoveEnemy();
        LookAt(_moveTarget);
        if (transform.position == _moveTarget)
        {
            _moveTarget = RandomPosition(gameObject.transform);
        }
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, _moveTarget, moveSpeed * Time.deltaTime);
    }

    private void LookAt(Vector3 target)
    {
        if (target.x > gameObject.transform.position.x)
        {
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, 0f, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, 180f, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
        }
    }
}

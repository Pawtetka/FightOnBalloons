using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : BossController
{
    [SerializeField] private float attackAngle;
    private GameObject _player;
    public override void StartValues()
    {
        base.StartValues();
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("TimeToCast");
    }

    public override void UpdateCode()
    {
        base.UpdateCode();
        if (Math.Abs(castAnims[1].transform.position.y - _player.transform.position.y) <= attackAngle)
        {
            if (bossState == BossState.Move)
            {
                bossState = BossState.SmallCast;
            }
        }
    }
    public override IEnumerator SmallCastCreate(GameObject castObject)
    {
        yield return new WaitForSeconds(0.5f);
        if (bossState != BossState.Death)
        {
            var newCast = Instantiate(castObject, castObject.transform.position,
            castObject.transform.rotation);
            newCast.GetComponent<Bullet>().ParentTag = gameObject.transform.tag;
            newCast.SetActive(true);
            Destroy(newCast, 3f);
            _animator.SetTrigger(BackToIdle);
            bossState = BossState.Move;
        }
        
    }

    public override IEnumerator CastCreate(GameObject lightning)
    {
        yield return new WaitForSeconds(0.5f);
        if (bossState != BossState.Death)
        {
            var pos = RandomPosition(lightning.transform);
            var newLightning = Instantiate(lightning, pos,
            Quaternion.identity, gameObject.transform.parent.parent);
            newLightning.SetActive(true);
            _animator.SetTrigger(BackToIdle);
            bossState = BossState.Move;
        }
    }

    private Vector3 RandomPosition(Transform transform)
    {
        var x = UnityEngine.Random.Range(-16, 16);
        var y = UnityEngine.Random.Range(-5, 7);
        var z = transform.position.z;
        return new Vector3(x, y, z);
    }

    private IEnumerator TimeToCast()
    {
        while (true)
        {
            yield return new WaitForSeconds(6);
            if (bossState == BossState.Move)
            {
                bossState = BossState.Cast;
            }
        }
    }
}

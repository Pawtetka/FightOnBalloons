using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FairyQueenController : BossController
{
    [SerializeField] private float shieldTime;
    private Vector3 _moveTarget;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject[] enemyPrefabs;
    private List<GameObject> enemies;
    [SerializeField] private GameObject portals;
    

    public override void StartValues()
    {
        base.StartValues();
        StartCoroutine("TimeToCast");
        StartCoroutine("TimeToSmallCast");
        enemies = new List<GameObject>();
    }
    
    public override IEnumerator SmallCastCreate(GameObject lightning)
    {
        yield return new WaitForSeconds(0.5f);
        lightning.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        CreateEnemies();
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (CheckEnemies())
            {
                DisableShield(lightning);
                break;
            }
        }
    }
    private void DisableShield(GameObject lightning)
    {
        lightning.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _animator.SetTrigger(BackToIdle);
        bossState = BossState.Move;
        StartCoroutine("TimeToSmallCast");
        StartCoroutine("TimeToCast");
    }

    private bool CheckEnemies()
    {
        if (enemies.All(enemy => enemy == null))
        {
            enemies = new List<GameObject>();
        }
        if (enemies.Count == 0)
        {
            return true;
        }
        return false;
    }

    private void CreateEnemies()
    {
        OpenClosePortals();
        Invoke("InstEnemies", 1);
        Invoke("OpenClosePortals", 2f);
    }
    private void InstEnemies()
    {
        for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++)
        {
            var portalPos = portals.transform.GetChild(i).gameObject.transform.position;
            var enemyPos = new Vector3(portalPos.x, portalPos.y - 1, portalPos.z);
            enemies.Add(Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, 3)],
                        enemyPos, Quaternion.identity, gameObject.transform.parent));
        }
    }
    private void OpenClosePortals()
    {
        for (int i = 0; i < portals.transform.childCount; i++)
        {
            var isActive = portals.transform.GetChild(i).gameObject.activeSelf;
            portals.transform.GetChild(i).gameObject.SetActive(!isActive);
        }
    }

    public override IEnumerator CastCreate(GameObject lightning)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3[] pos = new Vector3[UnityEngine.Random.Range(5, 15)];
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = RandomPosition(lightning.transform);
            var newLightning = Instantiate(lightning, pos[i],
            Quaternion.identity, gameObject.transform.parent.parent);
            newLightning.SetActive(true);
            Invoke("CastAudioEffect", 0.75f);
            Destroy(newLightning, 2f);
        }
        _animator.SetTrigger(BackToIdle);
        bossState = BossState.Move;
        StartCoroutine("TimeToCast");
    }

    private void CastAudioEffect()
    {
        FindObjectOfType<AudioManager>().Play(audioEffects[2]);
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
        transform.position = Vector3.MoveTowards(gameObject.transform.position, _moveTarget, 
            moveSpeed * Time.deltaTime);
    }

    private void LookAt(Vector3 target)
    {
        if (target.x > gameObject.transform.position.x)
        {
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, 0f, 
                gameObject.transform.rotation.z, gameObject.transform.rotation.w);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, 180f, 
                gameObject.transform.rotation.z, gameObject.transform.rotation.w);
        }
    }
    private IEnumerator TimeToCast()
    {
        yield return new WaitForSeconds(6);
        if (bossState == BossState.Move)
        {
            bossState = BossState.Cast;
        }
    }

    private IEnumerator TimeToSmallCast()
    {
        yield return new WaitForSeconds(20);
        if (bossState == BossState.Move)
        {
            bossState = BossState.SmallCast;
        }
        else
        {
            StartCoroutine("TimeToSmallCast");
        }
    }

    public override void OnCollisionEnterCode(Collision2D collision)
    {
        base.OnCollisionEnterCode(collision);
        _moveTarget = RandomPosition(gameObject.transform);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _moveTarget = RandomPosition(gameObject.transform);
    }

}

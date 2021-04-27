using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    [SerializeField] private int maxEnemyAttackCount;
    private int _attackEnemyCount;
    private bool _canNewEnemyAttack;
    public bool CanNewEnemyAttack { get { return _canNewEnemyAttack; } }
    void Update()
    {
        CheckEnemies();
        CanAttackNewCheck();
    }

    private void CheckEnemies()
    {
        _attackEnemyCount = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            var enemyAI = transform.GetChild(i).gameObject.GetComponent<EnemyAI>();
            if (enemyAI != null && enemyAI.IsAttack)
            {
                _attackEnemyCount += 1;
            }
        }
    }
    private void CanAttackNewCheck()
    {
        if (_attackEnemyCount < maxEnemyAttackCount)
        {
            _canNewEnemyAttack = true;
            return;
        }
        _canNewEnemyAttack = false;
    }
    public int AliveCount()
    {
        return transform.childCount;
    }

}

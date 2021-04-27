using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenCompanion : SkillCompanionAI
{
    private GameObject _enemies;
    private GameObject _player;
    [SerializeField] private GameObject ravenPrefab;
    private GameObject _targetEnemy;
    private GameObject raven;

    public override void ActivateCompanion()
    {
        base.ActivateCompanion();
        RavenSpawn();
    }
    private void RavenSpawn()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(ravenPrefab, _player.transform.position, _player.transform.rotation, gameObject.transform);
    }
}

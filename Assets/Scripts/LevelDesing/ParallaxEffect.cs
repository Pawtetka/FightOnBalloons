using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float parallaxEffectCo;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x * -parallaxEffectCo, transform.position.y, transform.position.z);
    } 
}

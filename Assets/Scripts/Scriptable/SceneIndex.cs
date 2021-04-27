using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "SceneIndex")]
public class SceneIndex : ScriptableObject
{
    [SerializeField] public int loadSceneIndex;
    
}

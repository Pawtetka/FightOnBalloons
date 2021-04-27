using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCompanionAI : MonoBehaviour
{
    private bool isUsable;
    [SerializeField] private float coolDown;
    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;
    }

    public void UseCompanion()
    {
        if (isUsable)
        {
            ActivateCompanion();
        }
    }
    public virtual void ActivateCompanion()
    {
        isUsable = false;
        Invoke(nameof(Usable), coolDown);
    }
    private void Usable()
    {
        isUsable = true;
    }
}

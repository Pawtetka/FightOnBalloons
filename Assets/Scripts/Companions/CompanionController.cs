using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private List<Companion> companions;
    private EquipmentManager _equipmentManager;
    private SkillCompanionAI skill;
    private GameObject _activeCompanion;
    void Start()
    {
        try
        {
            _equipmentManager = EquipmentManager.instance;
            CreateCompanion();
        }
        catch { }
    }


    public void UseSkill()
    {
        if (skill != null)
        {
            skill.UseCompanion();
        }
    }
    private void CreateCompanion()
    {
        var companion = _equipmentManager.currentEquipment[1];
        if (companion != null)
        {
            foreach (Companion comp in companions)
            {
                if (comp.name == companion.name)
                {
                    _activeCompanion = Instantiate(comp.prefab, this.transform);
                    return;
                }
            }
        }
    }

}

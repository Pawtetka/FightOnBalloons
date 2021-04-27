using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDown;
    [SerializeField] private CompanionController companion;
    private SkillCompanionAI skill;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.isDown = false;
    }

    void Update()
    {
        /*if (isDown)
        {
            player.VerticalMovement();
            player.ButtonDown = true;
        }
        else
        {
            player.ButtonDown = false;
        }*/
    }
}

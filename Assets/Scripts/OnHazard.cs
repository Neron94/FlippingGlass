using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHazard : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private int pikeDamage = 1;                  //урон по по кол-во стаканчиков при попадание на пику
    [SerializeField] private int glassPlus = 1;                   //бонус стаканчиков при подборе стаканчика

    private void Awake()
    {
        gameHandler = transform.GetComponent<GameHandler>();
    }
    public void JumpOnHazard(PlatformManager platWithHazard)
    {
        if(platWithHazard.typeHazard == PlatformManager.Hazard.abyss)
        {
            gameHandler.GameOver();
            print("GameOver");
        }
        else if(platWithHazard.typeHazard == PlatformManager.Hazard.pike)
        {
            gameHandler.GlassCountManage(pikeDamage, false);
        }
        else if(platWithHazard.typeHazard == PlatformManager.Hazard.bonusGlass)
        {
            gameHandler.GlassCountManage(glassPlus, true);
        }

    }   //Проверка и последствия при столкновении с препятсвием
}

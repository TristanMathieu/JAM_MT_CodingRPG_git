using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHudScript : MonoBehaviour
{
    public Text txName;
    public Text txLv;
    public Slider slHP;

    public Text txHP;


    public void SetHUD(Unit unit)
    {
        txName.text = unit.unitName;
        txLv.text = "Lv. " + unit.unitLv;
        slHP.maxValue = unit.chips;
        slHP.value = unit.beignet;
        txHP.text = "HP : " + unit.beignet;
    }

    public void SetHP(int newHP)
    {
        slHP.value = newHP;
        if (newHP < 0)
        {
            txHP.text = "HP : 0";
        }
        else
        {
            txHP.text = "HP : " + newHP;
        }
    }

}

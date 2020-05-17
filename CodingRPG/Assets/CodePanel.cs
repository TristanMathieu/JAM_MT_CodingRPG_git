using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{
    public BattleSystem myBS;
    public GameObject pn_this;
    public GameObject btnOpen;


    public static bool aEteAchete;

    private void Update()
    {
        if (aEteAchete)
        {
            btnCloseClick();
            aEteAchete = false;
        }
    }

    public void btnOpenClick()
    {
        btnOpen.SetActive(false);
        pn_this.SetActive(true);
    }

    public void btnCloseClick()
    {
        btnOpen.SetActive(true);
        pn_this.SetActive(false);
    }

}

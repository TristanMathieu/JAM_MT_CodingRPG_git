using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName; //unitname
    public int unitLv;//unitlv

    public static bool cpts1Unlock = true;
    public static bool cpts2Unlock = true;
    public bool cpts2Active = false;


    public int madelaine;//damage
    public int chips;//maxhp
    public int beignet;//currenthp
    public int orange;//crichance
    public int saumon;//critmult
    public int menthe;//durée buff
    public int the;//healing power
    public int poulet;//generation power code
    public int endive;//emmagasined code


    public bool TakeDamage(Unit attaquant)
    //Fonction appelée quand une unité prend des dégats , retourne vrai si l'unité est morte
    {
        if(cpts2Active) {
            beignet -= attaquant.madelaine * attaquant.saumon;
        }
        else {
            if (Random.Range(0, 100) < attaquant.orange) {
                beignet -= attaquant.madelaine * attaquant.saumon;
            }
            else {
                beignet -= attaquant.madelaine;
            }
        }

        if (beignet <= 0)
            return true;
        else
            return false;
    }

    public void cpts1()//heal
    {
        beignet += the;
        if (beignet > chips)
            beignet = chips;
    }

    public void cpts2()//crit up
    {
        cpts2Active = true;
        BattleSystem.finTourBuffCrit = BattleSystem.numeroTour + menthe;
    }

    public void endCpts2()
    {
        cpts2Active = false;
    }
}

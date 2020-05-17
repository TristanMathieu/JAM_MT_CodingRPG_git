using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_panel : MonoBehaviour
{
    public GameObject PanelDisplayer;

    public void btn_jouer_clicked()
    {
        PanelDisplayer.SetActive(false);
    }
}

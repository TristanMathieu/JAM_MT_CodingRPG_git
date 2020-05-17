using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Button btnWinCode;
    public Slider SlBarreProg;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text dialogueText;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    public BattleHudScript playerHud;
    public BattleHudScript enemyHud;


    public static int numeroTour = 0;
    public static int finTourBuffCrit;


    private bool lockerBtn; //empeche l'utilisation répétée de bouton, créant plusieurs fois l'effet

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        lockerBtn = false;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();


        SlBarreProg.value = playerUnit.endive;

        dialogueText.text = "Le " + enemyUnit.unitName + " attaque!!";
        
        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choisis une action :";
        lockerBtn = true;
    }



    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit);
        gainCode();


        enemyHud.SetHP(enemyUnit.beignet);
        dialogueText.text = "L'attaque a réussit!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        } else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EnemyTurn()
    {
        //gestion tours
        numeroTour++;
        if (numeroTour > finTourBuffCrit)
        {
            playerUnit.endCpts2();
        }


        dialogueText.text = "Attention! Une attaque de " + enemyUnit.unitName;
        gainCode();

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit);
        playerHud.SetHP(playerUnit.beignet);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "Vous avez vaincus!";
        }
        else
        {
            dialogueText.text = "Vous avez perdu!";
        }
    }


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN || !lockerBtn)
            return;

        lockerBtn = false;
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN || !lockerBtn)
            return;

        if (Unit.cpts1Unlock)
        {
            lockerBtn = false;
            StartCoroutine(PlayerHeal());
        }
    }

    public void OnBuffCrit()
    {
        if (state != BattleState.PLAYERTURN || !lockerBtn)
            return;

        if (Unit.cpts2Unlock)
        {
            lockerBtn = false;
            StartCoroutine(PlayerBuffCrit());
        }
    }

    IEnumerator PlayerBuffCrit()
    {
        playerUnit.cpts2();

        dialogueText.text = "Vous vous Buffez pendant " + playerUnit.menthe + " tours! Vous ne ferez que des critiques!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.cpts1();

        playerHud.SetHP(playerUnit.beignet);
        dialogueText.text = "Vous vous soignez!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void gainCode()
    {
        playerUnit.endive += playerUnit.poulet;
        if (playerUnit.endive >= 100)
        {
            playerUnit.endive = 100;
            btnWinCode.interactable = true;
        }
        SlBarreProg.value = playerUnit.endive;
        
    }

    //Panel Code
    public void clickMadelaine()
    {
        upStats(1);
    }

    public void clickChips()
    {
        upStats(2);
    }

    public void clickBeignet()
    {
        upStats(3);
    }
    public void clickOrange()
    {
        upStats(4);
    }
    public void clickSaumon()
    {
        upStats(5);
    }

    public void clickMenthe()
    {
        upStats(6);
    }
    public void clickThe()
    {
        upStats(7);
    }
    public void clickPoulet()
    {
        upStats(8);
    }
    public void clickEndive()
    {
        upStats(9);
    }

    public void upStats(int idItem)
    {
        switch (idItem)
        {
            case 1:
                playerUnit.madelaine++;
                break;

            case 2:
                playerUnit.chips++;
                break;

            case 3:
                playerUnit.beignet++;
                break;

            case 4:
                playerUnit.orange++;
                break;

            case 5:
                playerUnit.saumon++;
                break;

            case 6:
                playerUnit.menthe++;
                break;

            case 7:
                playerUnit.the++;
                break;

            case 8:
                playerUnit.poulet++;
                break;

            case 9:
                playerUnit.endive++;
                break;

            default:
                return;
        }

        playerUnit.endive = 0;
        btnWinCode.interactable = false;
        CodePanel.aEteAchete = true;
    }
}

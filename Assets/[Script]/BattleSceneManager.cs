using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    public List<GameObject> PokemonSlotInBattle;
    public GameObject MainBattleMenu;
    public GameObject AttackBattleMenu;

    public bool InBattleProgresion = false;


    //public Button AttackButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToAttackMenu()
    {
        MainBattleMenu.SetActive(false);
        AttackBattleMenu.SetActive(true);
    }
    public void ToMainMenu()
    {
        MainBattleMenu.SetActive(true);
        AttackBattleMenu.SetActive(false);
    }

    public void BattleProgression()
    {
        if(InBattleProgresion == false)
        {
            InBattleProgresion=true;
            //PokemonSlotInBattle[0].GetComponent<PokemonScript>().InputAttackCommand(PokemonSlotInBattle[0].GetComponent<PokemonScript>().attackIndex);
            StartCoroutine(E_BattleProgression());
        }
    }

    IEnumerator E_BattleProgression()
    {
        PokemonSlotInBattle[0].GetComponent<PokemonScript>().AttackCommand();

        yield return new WaitForSeconds(1);

        int enemyRandomIndex = Random.Range(0, 4);

        PokemonSlotInBattle[1].GetComponent<PokemonScript>().attackIndex = enemyRandomIndex; ///
        PokemonSlotInBattle[1].GetComponent<PokemonScript>().AttackCommand();

        yield return new WaitForSeconds(1);

        ToMainMenu();
        InBattleProgresion = false;
    }

}

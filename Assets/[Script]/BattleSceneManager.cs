using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    public List<GameObject> PokemonSlotInBattle;
    public GameObject MainBattleMenu;
    public GameObject AttackBattleMenu;


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
}

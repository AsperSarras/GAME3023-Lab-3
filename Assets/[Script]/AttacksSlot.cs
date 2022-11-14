using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttacksSlot : MonoBehaviour
{
    public PokemonScript Pokemon;
    public Attacks Attack;
    public int AttackIndex;

    public TMP_Text ButtonText;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        AttackUpdate();
        UpdateUi();
    }

    public void UpdateUi()
    {
        ButtonText.text = Attack.name;
    }

    public void AttackUpdate()
    {
        switch (AttackIndex)
        {
            case 0:
                Attack = Pokemon.ListAttacks[0];
                break;
            case 1:
                Attack = Pokemon.ListAttacks[1];
                break;
            case 2:
                Attack = Pokemon.ListAttacks[2];
                break;
            case 3:
                Attack = Pokemon.ListAttacks[3];
                break;
        }
    }

    public void AttackCommand()
    {
        Pokemon.InputAttackCommand(AttackIndex);
    }
}

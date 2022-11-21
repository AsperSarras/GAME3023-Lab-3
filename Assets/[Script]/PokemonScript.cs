using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonScript : MonoBehaviour
{
    public BattleSceneManager battleSceneManager;

    public Pokemon pokemon;

    public List<Attacks> ListAttacks;

    public Image PokemonSprite;
    public Animator PokemonAnimations;
    public bool isPlayerPokemon = false;
    public int attackIndex = -1;

    // Start is called before the first frame update
    void Start()
    {

        PokemonSprite = GetComponent<Image>();

        if (gameObject == battleSceneManager.PokemonSlotInBattle[0])
        {
            //pokemon.isPlayerPokemon = true;
            PokemonSprite.sprite = pokemon.poke1;

            ListAttacks.Add(pokemon.attack0);
            ListAttacks.Add(pokemon.attack1);
            ListAttacks.Add(pokemon.attack2);
            ListAttacks.Add(pokemon.attack3);

        }

        //PokemonBattleStartUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        PokemonAnimations.SetBool("isPlayerPokemon", isPlayerPokemon);
        if (pokemon != null)
        {
            if (gameObject == battleSceneManager.PokemonSlotInBattle[1])
            {

                PokemonBattleStartUpdate();
            }
        }

    }

    public void PokemonBattleStartUpdate()
    {
        //pokemon = pokemonS;

        PokemonSprite.sprite = pokemon.poke2;

        ListAttacks.Add(pokemon.attack0);
        ListAttacks.Add(pokemon.attack1);
        ListAttacks.Add(pokemon.attack2);
        ListAttacks.Add(pokemon.attack3);

        //if (gameObject == battleSceneManager.PokemonSlotInBattle[1])
        //{
        //    //pokemon.isPlayerPokemon = true;
        //    PokemonSprite.sprite = pokemon.poke2;

        //    ListAttacks.Add(pokemon.attack0);
        //    ListAttacks.Add(pokemon.attack1);
        //    ListAttacks.Add(pokemon.attack2);
        //    ListAttacks.Add(pokemon.attack3);
        //}
    }

    public void InputAttackCommand(int i)
    {

        attackIndex = i;
        battleSceneManager.BattleProgression();
    }

    public void AttackCommand()
    {
        if (isPlayerPokemon)
        {
            PokemonAnimations.Play(ListAttacks[attackIndex].animationNamePlayer);
        }
        else
        {
            PokemonAnimations.Play(ListAttacks[attackIndex].animationNameEnemy);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonScript : MonoBehaviour
{
    public BattleSceneManager battleSceneManager;

    public Pokemon pokemon;
    public Image PokemonSprite;
    public Animator PokemonAnimations;

    // Start is called before the first frame update
    void Start()
    {
        PokemonSprite = GetComponent<Image>();
        if(gameObject == battleSceneManager.PokemonSlotInBattle[0])
        {
            pokemon.isPlayerPokemon = true;
            PokemonSprite.sprite = pokemon.poke1;
        }
        else if(gameObject == battleSceneManager.PokemonSlotInBattle[1])
        {
            pokemon.isPlayerPokemon = true;
            PokemonSprite.sprite = pokemon.poke2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

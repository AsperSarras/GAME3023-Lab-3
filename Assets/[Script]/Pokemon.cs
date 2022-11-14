using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/New Pokemon")]
public class Pokemon : ScriptableObject 
{
    public Sprite poke1;
    public Sprite poke2;

    public Attacks attack0;
    public Attacks attack1;
    public Attacks attack2;
    public Attacks attack3;


    //public bool isPlayerPokemon;
}

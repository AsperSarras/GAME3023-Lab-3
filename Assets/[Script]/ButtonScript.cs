using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //To toggle the battle scene on and off
    public GameObject BattlseScene;

    public BattleSceneManager BattleSceneManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void flee()
    {
        if (BattlseScene.gameObject.activeSelf == true)
        {
            BattlseScene.SetActive(false);
        }
    }
}

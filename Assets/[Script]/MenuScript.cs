using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void StartGame()
    {
        Singleton.Instance.Load = false;
        SceneManager.LoadScene(1); 
    }
    public void ContinueGame()
    {
        if (File.Exists(@"..\GAME3023-Lab-3\SaveData.txt"))
        {
            Singleton.Instance.Load = true;
            SceneManager.LoadScene(1);
        }

    }


}

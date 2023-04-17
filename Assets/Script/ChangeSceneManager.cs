using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToDeath()
    {
        
        
        SceneManager.LoadScene(2);
        
    }
    public void ToVictory()
    {
        SceneManager.LoadScene(3);
    }
}

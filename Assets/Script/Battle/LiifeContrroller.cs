using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiifeContrroller : MonoBehaviour
{
    public int totallifes;
   static int currentlifes;
    public bool died;
    public Text numvida;
    public GameObject SceneManager, Player;
    SpriteRenderer boi;
    void Start()
    {
        currentlifes = totallifes;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        string _crlifes = currentlifes.ToString();
      numvida.text = _crlifes;
        if (currentlifes <= 0)
        {
            died = true;
            currentlifes = 0;
            StartCoroutine(DeathEpilogue(1f));
        }

    }
    public static void CheckLifes5()
    {
        currentlifes = currentlifes - 5;
        GameObject newPlayer = GameObject.Find("Player");
        SpriteRenderer newboi = GameObject.Find("sprite_0").GetComponent<SpriteRenderer>();
        //Debug.Log(currentlifes);
        newPlayer.GetComponent<PlayerController>().PaintPlayer();
      
        
    }

    public static void CheckLifes15()
    {
        currentlifes = currentlifes - 15;
        GameObject newPlayer = GameObject.Find("Player");
        SpriteRenderer newboi = GameObject.Find("sprite_0").GetComponent<SpriteRenderer>();
        //Debug.Log(currentlifes);
        newPlayer.GetComponent<PlayerController>().PaintPlayer();

    }
    IEnumerator DeathEpilogue(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.GetComponent<ChangeSceneManager>().ToDeath();

        //print("Basado y rojopileado");
    }

}


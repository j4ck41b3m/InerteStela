using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("Intro");
        anim.SetTrigger("Victory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using System.Collections;

public class TuukanBlaa : MonoBehaviour {

    public Animator anim;


    bool running;

    void Update ()
    {
        if (running)
        {
            // juoksu juttuja
            anim.SetBool("running", true);
        } else
        {
            anim.SetBool("running", false);
            
        }
    }
}

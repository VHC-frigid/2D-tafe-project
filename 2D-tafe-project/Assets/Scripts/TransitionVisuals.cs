using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionVisuals : MonoBehaviour
{
    
    public static TransitionVisuals transitioner;

    void Start()
    {
        transitioner = this;
    }

    public void StartTransition()
    {
        
    }
}

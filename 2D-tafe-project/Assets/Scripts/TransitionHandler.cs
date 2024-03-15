using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DitherFadeType
{
    In,
    Out
}

public class TransitionHandler : MonoBehaviour
{
    public static TransitionHandler instance;

    [SerializeField] private Image ditherImage;
    [SerializeField] private Animator ditherAnimator;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Fade(DitherFadeType.In);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Fade(DitherFadeType.Out);
        }
    }

    public void Fade(DitherFadeType type)
    {
        switch (type)
        {
            case DitherFadeType.In:
                ditherAnimator.SetBool("FadingIn", true);
                ditherAnimator.SetBool("FadingOut", false);
                break;
            case DitherFadeType.Out:
                ditherAnimator.SetBool("FadingOut", true);
                ditherAnimator.SetBool("FadingIn", false);
                break;
            default:
                Debug.LogWarning("Unknown fade type");
                break;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeAction : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                mAnimator.SetTrigger("TrDig");
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                mAnimator.SetTrigger("TrRun");
            }
        }
    }
}

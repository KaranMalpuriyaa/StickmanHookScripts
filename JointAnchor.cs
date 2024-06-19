using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAnchor : MonoBehaviour
{
    [SerializeField] private Sprite spriteUnsticked;
    [SerializeField] private Sprite spriteSticked;

    public float animTime;
    public AnimationCurve animationCurve;

    private SpriteRenderer spriteRenderer;
    private GameObject dashLine;
    private bool sticked = false;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        dashLine = gameObject.transform.GetChild (1).gameObject;
    }


    public void SetSticked ()
    {
        spriteRenderer.sprite = spriteSticked;
        sticked = true;
    }

    public void SetUnsticked ()
    {
        spriteRenderer.sprite = spriteUnsticked;
        sticked = false;
        Unselected ();
    }

    public void Selected ()
    {
        if(!sticked)
        {
            StartCoroutine (SelectingJoint ());
        }
        else
        {
            dashLine.transform.localScale = Vector3.zero;
        }
    }

    public void Unselected ()
    {
        StopCoroutine (SelectingJoint ());
        dashLine.transform.localScale = Vector3.zero;
    }

    IEnumerator SelectingJoint ()
    {
        float time = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = new Vector3 (1.2f, 1.2f, 1f);
        while(time <= animTime)
        {
            time += Time.deltaTime;
            dashLine.transform.localScale = Vector3.Lerp (startScale, endScale, animationCurve.Evaluate (time));
            yield return null;
        }
    }
}
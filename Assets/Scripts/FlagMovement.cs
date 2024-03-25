using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagMovement : MonoBehaviour
{
    public Sprite[] mySprites;
    private int index = 0;

    private SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(IdleCoRoutine());
        
    }


    IEnumerator IdleCoRoutine(){
        yield return new WaitForSeconds(0.08f);
        mySpriteRenderer.sprite = mySprites[index];
        index++;
        if(index == mySprites.Length){
            index = 0;
        }
        StartCoroutine(IdleCoRoutine());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer mySR;
    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        mySR.sortingOrder = (int)(-10 * transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

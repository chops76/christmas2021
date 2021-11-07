using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float deceleration = 5f;
    public float lifetime = 3f;
    public float fadeSpeed = 2.5f;

    private Vector3 moveDirection;
    private SpriteRenderer mySR;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
        mySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Color newColor = mySR.color;
            if (newColor.a <= 0)
            {
                Destroy(gameObject);
            }
            newColor.a = Mathf.MoveTowards(newColor.a, 0, fadeSpeed * Time.deltaTime);
            mySR.color = newColor;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomber : MonoBehaviour
{
    public bool goingUp;
    public float speed;
    private GameManager gameManager;
    // Update is called once per frame
    void Start()
    {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if(goingUp == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        if(transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }

    }
}

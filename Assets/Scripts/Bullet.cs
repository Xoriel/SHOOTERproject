using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform translate - movement without physics direction, time, speed
        //all floats need f by it if it's the number
        transform.Translate(new Vector3 (0,1,0) * Time.deltaTime * 8f);
        //when the bullet is high enough, destroy it
        //if statements check things, if they're true, the code in the block works, if they're false, the code in the block is ignored
        if(transform.position.y > 6.5f)
        {
            Destroy(this.gameObject);
        }
    }
}

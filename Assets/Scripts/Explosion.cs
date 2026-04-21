using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{  
     private GameManager gameManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
         gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
         gameManager.PlaySound(5);   
         Destroy(this.gameObject, 2.5f);
        }

         // Update is called once per frame
        void Update()
        {
        
        }
}

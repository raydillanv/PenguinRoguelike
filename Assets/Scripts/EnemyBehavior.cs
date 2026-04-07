using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
   public Transform Player; 
   public float ChaseSpeed;
   public float ReturnDistance;
   public GameObject Enemy;
   
   public bool Flee;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {

        float distance = Vector2.Distance(transform.position, Player.position);

        if(!Flee) {
             transform.position = Vector2.MoveTowards(transform.position, Player.position, ChaseSpeed * Time.deltaTime);
             } else {
                if(distance > ReturnDistance) Flee = false;
                transform.position = Vector2.MoveTowards(transform.position, Player.position, -1 * ChaseSpeed * Time.deltaTime);
             }
    }

        public void OnCollisionEnter2D(Collision2D collider) {
                if(collider.gameObject.tag == "Player") {
                    Flee = true;
                }
            }
    } 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandMovement : MonoBehaviour
{

    public Transform firstPos, secondPos;
    private Vector3 nextPos;
    public float speed = 4f;

    //public GameObject Player;

    //private Vector3 velocity;

    //public bool move;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = firstPos.transform.position;
        //Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == firstPos.transform.position)
        {
            nextPos = secondPos.position;
        }
        else if(transform.position == secondPos.transform.position)
        {
            nextPos = firstPos.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(firstPos.position, secondPos.position);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        move = true;
    //        collision.transform.SetParent(transform);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //        move = false;
    //        collision.transform.SetParent(null);
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if(move)
    //    {
    //        transform.position += (velocity * Time.deltaTime);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject == Player)
    //    {
    //        Player.transform.parent = gameObject.transform;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject == Player)
    //    {
    //        Player.transform.parent = null;
    //    }
    //}
}

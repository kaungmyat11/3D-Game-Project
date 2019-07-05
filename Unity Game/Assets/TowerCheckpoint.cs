using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCheckpoint : MonoBehaviour
{
    public Vector3 checkposition;

    public GameObject player;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        checkposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player enter");
            player.GetComponent<PlayerController>().checkPoint = checkposition;
            animator.enabled = true;
        }
    }
}

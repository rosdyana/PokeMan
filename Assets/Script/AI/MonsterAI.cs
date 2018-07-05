using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform MainChar;
    public NavMeshAgent agent;
    private new Animation animation;
    private bool hitPacman = false;
    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!hitPacman)
        {
            MainChar = GameObject.FindGameObjectWithTag("Pacman").transform;
            agent.SetDestination(MainChar.position);
            animation.Play("Zombie Run");
        }



    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Pacman")
        {
            // todo if pacman got powerup
            //animation.Stop();
            //animation.Play("Zombie Death");
            //StartCoroutine(DelayDeactivate());
            animation.Stop();
            hitPacman = true;
            animation.Play("Victory");
        }
    }
    IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}

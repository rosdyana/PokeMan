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
    PacmanController pcInstance = null;
    // Use this for initialization
    void Start()
    {
        GameObject tempObj = GameObject.FindGameObjectWithTag("Pacman");
        pcInstance = tempObj.GetComponent<PacmanController>();

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
            if (pcInstance.isPowerUp)
            {
                animation.Stop();
                hitPacman = true;
                animation.Play("Zombie Death");
                StartCoroutine(DelayDeathPose());
            }
            else
            {
                animation.Stop();
                hitPacman = true;
                animation.Play("Victory");
                if (pcInstance.PacmanHealth != 0)
                    StartCoroutine(DelayVictoryPose());
            }
        }
    }

    IEnumerator DelayVictoryPose()
    {
        yield return new WaitForSeconds(5f);
        hitPacman = false;
    }

    IEnumerator DelayDeathPose()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

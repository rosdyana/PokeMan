using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private new Animation animation;
    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Pacman")
        {
            animation.Stop();
            animation.Play("Zombie Death");
            StartCoroutine(DelayDeactivate());
        }
    }
    IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}

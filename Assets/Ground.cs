using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    //public GameObject ground;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hier");
        if (collision.gameObject.CompareTag("Player"))
        {
            /**
            if (gameObject.name.Contains("Switch"))
            {
                ground.SetActive(true);
                gameObject.SetActive(false);
            }
            **/

            Debug.Log("Hit");
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.ResetJump(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Left collision");
                player.ResetJump(false);
            }
        }
    }

    
}

using UnityEngine;

public class AppearBlocks : MonoBehaviour
{
    public GameObject[] objectsToEnable; 
    private Collider2D[] blockColliders; 

    void Start()
    {
        blockColliders = new Collider2D[objectsToEnable.Length];
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            blockColliders[i] = objectsToEnable[i].GetComponent<Collider2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
                
                if (obj.CompareTag("Bloque"))
                {
                    Collider2D blockCollider = obj.GetComponent<Collider2D>();
                    if (blockCollider != null)
                    {
                        blockCollider.enabled = false;
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(false);
                
                if (obj.CompareTag("Bloque"))
                {
                    Collider2D blockCollider = obj.GetComponent<Collider2D>();
                    if (blockCollider != null)
                    {
                        blockCollider.enabled = true;
                    }
                }
            }
        }
    }
}

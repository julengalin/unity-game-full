using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearBlocks : MonoBehaviour
{
    public GameObject[] objectsToDisable; // Array de GameObjects a desactivar
    private Collider2D[] blockColliders; // Array de colliders de los bloques

    void Start()
    {
        // Obtener todos los colliders de los bloques
        blockColliders = new Collider2D[objectsToDisable.Length];
        for (int i = 0; i < objectsToDisable.Length; i++)
        {
            blockColliders[i] = objectsToDisable[i].GetComponent<Collider2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desactivar los colliders de los bloques cuando el jugador esté encima de ellos
            foreach (Collider2D collider in blockColliders)
            {
                collider.enabled = false;
            }

            // Desactivar los objetos (Idle(2), Idle(3) y Idle(4)) cuando el jugador esté encima de los bloques
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
                // Si el objeto es uno de los bloques, desactivar su collider
                if (obj.name == "Idle (2)" || obj.name == "Idle (3)" || obj.name == "Idle (4)")
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
            // Activar los colliders de los bloques cuando el jugador deje de estar encima de ellos
            foreach (Collider2D collider in blockColliders)
            {
                collider.enabled = true;
            }

            // Activar los objetos (Idle(2), Idle(3) y Idle(4)) cuando el jugador deje de estar encima de los bloques
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(true);
                // Si el objeto es uno de los bloques, activar su collider
                if (obj.name == "Idle (2)" || obj.name == "Idle (3)" || obj.name == "Idle (4)")
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

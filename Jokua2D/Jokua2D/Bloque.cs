using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{
    using UnityEngine;

    public class Bloque : MonoBehaviour
    {
        public GameObject fallingPlatform; // Referencia a la plataforma que cae

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Desactivar la plataforma cuando el jugador esté encima del bloque
                fallingPlatform.SetActive(false);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Activar la plataforma cuando el jugador deja de estar encima del bloque
                fallingPlatform.SetActive(true);
            }
        }
    }

}

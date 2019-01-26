using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpawn : MonoBehaviour
{
        public float babyNumber;
        public PlayerController playerPossum;
        SpriteRenderer babyShow;

        private void Start()
        {
            playerPossum = GetComponentInParent<PlayerController>();
            babyShow = GetComponent<SpriteRenderer>();
        }

        void Update ()
        {
        //muestra a este bebe si la cantidad de bebes del player es correcta.
            if (playerPossum.babyCount >= babyNumber)
            {
            babyShow.enabled = true;
            }

        else
        {
            babyShow.enabled = false;
        }
        }
}

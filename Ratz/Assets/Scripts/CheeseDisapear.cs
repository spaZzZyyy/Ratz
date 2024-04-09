using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseDisapear : MonoBehaviour
{
    [SerializeField] GameObject cheese;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            cheese.SetActive(false);
        }
    }
}

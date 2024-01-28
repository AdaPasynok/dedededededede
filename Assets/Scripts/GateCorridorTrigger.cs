using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCorridorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<Animator>().SetTrigger("Unfold");
            gameObject.SetActive(false);
        }
    }
}

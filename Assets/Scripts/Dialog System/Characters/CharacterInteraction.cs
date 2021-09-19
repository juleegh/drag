using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    private Character current;

    void OnTriggerEnter(Collider other)
    {
        Character next = other.GetComponent<Character>();
        if (next != null)
        {
            current = next;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Character next = other.GetComponent<Character>();
        if (next != null && next == current)
        {
            current = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && current != null)
        {
            if (!DialogSystemController.Instance.IsInteracting)
                DialogSystemController.Instance.StartInteraction(current);
        }
    }
}

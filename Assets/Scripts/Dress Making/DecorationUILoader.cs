using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationUILoader : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Decoration Decoration in Inventory.Instance.decorations.Values)
        {
            EmbelishmentButton button = Instantiate(buttonPrefab).GetComponent<EmbelishmentButton>();
            button.Initialize(Decoration);
            button.transform.SetParent(container);
        }
    }
}

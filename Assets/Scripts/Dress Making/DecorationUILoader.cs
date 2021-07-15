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
        foreach (DecorationInfo Decoration in Inventory.Instance.decorations.Values)
        {
            AccesoryButton button = Instantiate(buttonPrefab).GetComponent<AccesoryButton>();
            button.Initialize(Decoration);
            button.transform.SetParent(container);
        }
    }
}

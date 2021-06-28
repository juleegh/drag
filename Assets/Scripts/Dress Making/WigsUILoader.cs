using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigsUILoader : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Wig Wig in WigFitter.Instance.Wigs.Values)
        {
            AccesoryButton button = Instantiate(buttonPrefab).GetComponent<AccesoryButton>();
            button.Initialize(Wig);
            button.transform.SetParent(container);
        }
    }
}

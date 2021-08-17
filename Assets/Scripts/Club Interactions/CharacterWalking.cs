using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalking : MonoBehaviour, RequiredComponent
{
    [SerializeField] private float speed;

    public void ConfigureRequiredComponent()
    {
        //PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, PossesPlayer);
    }

    public void PossesPlayer()
    {
        GlobalPlayerManager.Instance.gameObject.SetActive(false);
        GlobalPlayerManager.Instance.transform.SetParent(this.transform);
        GlobalPlayerManager.Instance.transform.localPosition = Vector3.zero;
        GlobalPlayerManager.Instance.transform.eulerAngles = Vector3.zero;
        GlobalPlayerManager.Instance.gameObject.SetActive(true);
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.LookAt(transform.position + movement);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}

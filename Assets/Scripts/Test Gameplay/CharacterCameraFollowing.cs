using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class CharacterCameraFollowing : MonoBehaviour
    {
        [SerializeField] private Vector3 distanceFromFocus;
        [SerializeField] private float panSpeed;
        [SerializeField] private bool followCharacter = false;

        void Awake()
        {
            if (followCharacter)
            {
                Vector3 objective = BattleSectionManager.Instance.InTurn.transform.position + distanceFromFocus;
                transform.position = objective;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (followCharacter)
            {
                Vector3 objective = BattleSectionManager.Instance.InTurn.transform.position + distanceFromFocus;
                transform.position = Vector3.MoveTowards(transform.position, objective, Time.deltaTime * panSpeed);
            }
        }
    }
}

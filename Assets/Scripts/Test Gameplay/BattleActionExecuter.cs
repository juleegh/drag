using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;

namespace TestGameplay
{
    public class BattleActionExecuter : MonoBehaviour
    {
        [Serializable]
        public class ActionsDictionary : SerializableDictionaryBase<ActionInput, BattleAction> { }

        protected BattleActionType currentActionType;
        public BattleActionType CurrentActionType { get { return currentActionType; } }
        [SerializeField] protected ActionsDictionary moveActions;
        [SerializeField] protected ActionsDictionary attackActions;
        [SerializeField] protected ActionsDictionary defenseActions;
        [SerializeField] protected ActionsDictionary specialActions;

        public ActionsDictionary MoveActions { get { return moveActions; } }
        public ActionsDictionary AttackActions { get { return attackActions; } }
        public ActionsDictionary DefenseActions { get { return defenseActions; } }
        public ActionsDictionary SpecialActions { get { return specialActions; } }

        public List<Vector2Int> GetTargetPositions(ActionInput actionInput)
        {

            switch (currentActionType)
            {
                case BattleActionType.Attack:
                    return attackActions[actionInput].TargetDirections;
                case BattleActionType.Move:
                    return moveActions[actionInput].TargetDirections;
                case BattleActionType.Defend:
                    return defenseActions[actionInput].TargetDirections;
                case BattleActionType.Special:
                    return specialActions[actionInput].TargetDirections;
            }

            return new List<Vector2Int>();
        }

        public BattleAction GetActionByPosition(Vector2Int delta)
        {
            ActionsDictionary targetDictionary = null;
            switch (currentActionType)
            {
                case BattleActionType.Attack:
                    targetDictionary = AttackActions;
                    break;
                case BattleActionType.Move:
                    targetDictionary = MoveActions;
                    break;
                case BattleActionType.Defend:
                    targetDictionary = DefenseActions;
                    break;
                case BattleActionType.Special:
                    targetDictionary = SpecialActions;
                    break;
            }

            foreach (BattleAction action in targetDictionary.Values)
            {
                if (action.TargetDirections.Contains(delta))
                    return action;
            }

            return null;
        }

        public BattleAction GetActionByInput(ActionInput input)
        {
            switch (currentActionType)
            {
                case BattleActionType.Attack:
                    return AttackActions.ContainsKey(input) ? AttackActions[input] : null;
                case BattleActionType.Move:
                    return MoveActions.ContainsKey(input) ? MoveActions[input] : null;
                case BattleActionType.Defend:
                    return DefenseActions.ContainsKey(input) ? DefenseActions[input] : null;
                case BattleActionType.Special:
                    return SpecialActions.ContainsKey(input) ? SpecialActions[input] : null;
            }

            return null;
        }

    }
}

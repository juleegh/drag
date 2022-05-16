using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{

    public class BattleRespawn : MonoBehaviour
    {
        private static BattleRespawn instance;
        public static BattleRespawn Instance { get { return instance; } }

        private Dictionary<BattleCharacter, Vector2Int> respawnPositions;

        void Awake()
        {
            instance = this;
            respawnPositions = new Dictionary<BattleCharacter, Vector2Int>();
        }

        public void SetCheckpoint(BattleCharacter character, Vector2Int respawnPos)
        {
            respawnPositions[character] = respawnPos;
        }

        public void RespawnCharacters()
        {
            foreach (KeyValuePair<BattleCharacter, Vector2Int> character in respawnPositions)
            {
                Vector3 worldPos = new Vector3(character.Value.x, 0, character.Value.y);
                character.Key.transform.position = worldPos;
                character.Key.Initialize();
            }

            BattleSectionManager.Instance.ResetTurns();
        }
    }
}

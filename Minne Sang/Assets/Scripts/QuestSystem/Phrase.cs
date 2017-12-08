using UnityEngine;

[System.Serializable]
public class Phrase {
    [Tooltip("Whether This Phrase spoken by the npc (not the player)")]
    public bool spokenByNpc;
    public bool messageByPrincess;

    [Tooltip("The Spoken words")]
    public string text;
}

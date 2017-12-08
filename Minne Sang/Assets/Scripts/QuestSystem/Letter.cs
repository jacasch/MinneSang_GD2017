using UnityEngine;

[System.Serializable]
public class Letter : Phrase
{
    [Tooltip("Whether This Phrase spoken by the princess (not the player)")]
    public static bool messageByPrincess; 
}

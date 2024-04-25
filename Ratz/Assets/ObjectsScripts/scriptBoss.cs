using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Boss Data")]
public class scriptBoss : ScriptableObject
{
    public GameObject bossPrefab;
    public int attackPattern;
    public int attackBeats;
    
}

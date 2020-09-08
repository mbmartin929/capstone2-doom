using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk Generator/Player/Create Perk")]
public class Perk : ScriptableObject
{

    public string description;
    public Sprite icon;
    public int goldNeeded;

    public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();
}

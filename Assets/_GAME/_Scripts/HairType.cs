using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "HairType", menuName = "Lice - Day1/HairType", order = 0)]
public class HairType : ScriptableObject

{
    public enum HairTypes
    {
        Wavy_Long, Wavy_Short, Flat_Short, Spikey, Male1, Male2, Male3
    }
    public HairTypes hairType;

}

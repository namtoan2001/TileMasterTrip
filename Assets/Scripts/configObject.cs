using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "configObject", menuName = "ScriptableObjects/configObject", order = 1)]
public class configObject : ScriptableObject
{
    public List<TypeConfig> typeDetails;

}

using System;
using UnityEngine;

public class TypeDetails : MonoBehaviour
{
    public int Id;
    public bool checkClick = false;
    public Sprite sprite;

}
[Serializable]
public class TypeConfig
{
    public GameObject prefab;
    public int Id;
    public bool checkClick = false;
    public Sprite sprite;
}

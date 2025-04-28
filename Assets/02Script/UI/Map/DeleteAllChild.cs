using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAllChild : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private MapMarkInGame map;

    public void AllDelete()
    {
        foreach(Transform child in parentObject.transform)
        {
            Destroy(child.gameObject);

            map.ResetMapMarkNum();
        }
    }
}

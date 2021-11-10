using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectHelper
{
    static public GameObject[] GetChildsByTag(GameObject obj, string tag)
    {
        Transform tr = obj.transform;
        List<GameObject> result = new List<GameObject>();

        for(int i = 0 ; i < tr.childCount ; ++i)
        {
            GameObject child = tr.GetChild(i).gameObject;

            if(child.tag == tag)
                result.Add(child);
        }

        return result.ToArray();
    }
}

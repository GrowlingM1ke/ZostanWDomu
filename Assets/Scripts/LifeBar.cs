using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    private static List<Transform> brains = new List<Transform>();
    public static Transform brain;

    private void Awake()
    {
        brain = gameObject.transform.Find("LifeBar") as Transform;
        if (brain == null)
            Debug.LogError("Couldn't find lifebar transform");

    }


    public static void SetLife(int num)
    {
        foreach (Transform t in brains)
        {
            Destroy(t.gameObject);
        }
        brains.Clear();
        for (int i = 0; i < num; i++)
        {
            Transform sp = Instantiate(brain);
            sp.parent = brain.parent;
            sp.position = brain.position;
            sp.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            sp.position = new Vector3(sp.position.x + i * 4, sp.position.y, sp.position.z);
            brains.Add(sp);
        }


    }
}

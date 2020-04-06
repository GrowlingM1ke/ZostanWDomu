using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPortalColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite secondSprite;
    void Start()
    {
        if (gameObject.name == "Portal_sluch" && GameObject.Find("information").GetComponent<information>().finishedSluch == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }
        if (gameObject.name == "Portal_dotyk" && GameObject.Find("information").GetComponent<information>().finishedDotyk == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }
        if (gameObject.name == "Portal_wzrok" && GameObject.Find("information").GetComponent<information>().finishedWzrok == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }
        if (gameObject.name == "Portal_ruch" && GameObject.Find("information").GetComponent<information>().finishedRuch == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }

    }
}

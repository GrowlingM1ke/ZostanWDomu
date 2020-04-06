using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPortalColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite secondSprite;
    void Start()
    {
        if (gameObject.name == "Portal_sluch" && GameObject.Find("Information").GetComponent<information>().finishedSluch == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }
        if (gameObject.name == "Portal_dotyk" && GameObject.Find("Information").GetComponent<information>().finishedDotyk == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }
        if (gameObject.name == "Portal_wzrok" && GameObject.Find("Information").GetComponent<information>().finishedWzrok == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }
        if (gameObject.name == "Portal_ruch" && GameObject.Find("Information").GetComponent<information>().finishedRuch == 2)
        {
            gameObject.tag = "nullifier";
            gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        }

    }
}

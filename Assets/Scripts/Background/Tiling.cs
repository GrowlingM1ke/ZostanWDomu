using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offset = 4;

    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool hasAUpBuddy = false;
    public bool hasADownBuddy = false;

    public bool reverseScale = false;

    private float spriteWidth = 0f;
    private float spriteHeight = 0f;
    private Camera cam;
    private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x * System.Math.Abs(sRenderer.transform.localScale.x);
        spriteHeight = sRenderer.sprite.bounds.size.y * System.Math.Abs(sRenderer.transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasALeftBuddy == false || hasARightBuddy == false || hasAUpBuddy == false || hasADownBuddy == false)
        {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
            float camVerticalExtend = cam.orthographicSize * Screen.height / Screen.width;

            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            float edgeVisiblePositionUp = (myTransform.position.y + spriteHeight / 2) - camVerticalExtend;
            float edgeVisiblePositionDown = (myTransform.position.y - spriteHeight / 2) + camVerticalExtend;

            if (cam.transform.position.x >= edgeVisiblePositionRight - offset && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if(cam.transform.position.x <= edgeVisiblePositionLeft + offset && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
            else if (cam.transform.position.y >= edgeVisiblePositionUp - offset && hasAUpBuddy == false)
            {
                MakeNewBuddy(2);
                hasAUpBuddy = true;
            }
            else if (cam.transform.position.y <= edgeVisiblePositionDown + offset && hasADownBuddy == false)
            {
                MakeNewBuddy(-2);
                hasADownBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int rightOrLeft)
    {
        if (System.Math.Abs(rightOrLeft) == 1)
        {
            Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
            Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

            if (reverseScale == true)
            {
                newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
            }

            newBuddy.parent = myTransform.parent;

            if (rightOrLeft > 0)
            {
                newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
            }
            else
            {
                newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
            }
        }
        else
        {
            Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + spriteHeight * rightOrLeft / 2, myTransform.position.z);
            Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

            if (reverseScale == true)
            {
                newBuddy.localScale = new Vector3(newBuddy.localScale.x, newBuddy.localScale.y * -1, newBuddy.localScale.z);
            }

            newBuddy.parent = myTransform.parent;

            if (rightOrLeft > 0)
            {
                newBuddy.GetComponent<Tiling>().hasADownBuddy = true;
            }
            else
            {
                newBuddy.GetComponent<Tiling>().hasAUpBuddy = true;
            }
        }
    }
}

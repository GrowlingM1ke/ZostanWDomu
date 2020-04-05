using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformTouch : MonoBehaviour
{
    private bool firstCase = true;
    public float Maxtime = 5f;
    private float timer = -1f;
    public int inc = 1;
    public int state = 1;
    private float effect = 0f;
    public float heal = 1f;
    public float damage = -2f;

    // Update is called once per frame
    void Update()
    {
        if (firstCase)
        {
            firstCase = !firstCase;
            randStart();
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = Maxtime;
            inc += Random.Range(0, 2) == 0 ? -1 : 1;
            state = Mathf.Abs(inc % 3);

            switch (state)
            {
                case 0:
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(114, 226, 0);
                    effect = heal;
                    break;
                case 1:
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                    effect = 0;
                    break;
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                    effect = damage;
                    break;
            }
        }
        
    }

    public float takeEffect()
    {
        return effect;
    }




    public IEnumerator randStart()
    {
        yield return new WaitForSeconds(Random.Range(0f, 4f));
    }
}

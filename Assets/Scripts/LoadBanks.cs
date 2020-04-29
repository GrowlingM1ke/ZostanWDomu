using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBanks : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(waitForBanks());
    }


    private IEnumerator waitForBanks()
    {
        while (!FMODUnity.RuntimeManager.HasBanksLoaded)
            yield return null;
        SceneManager.LoadScene("Intro");
    }
}

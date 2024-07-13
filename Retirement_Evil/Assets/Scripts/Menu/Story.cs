using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public int numScene;

    public void Continue() {
        SceneManager.LoadSceneAsync(numScene);
    }
}

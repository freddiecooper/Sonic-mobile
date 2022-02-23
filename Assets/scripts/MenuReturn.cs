using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuReturn : MonoBehaviour
{
    public void goBack()
    {
        SceneManager.LoadScene(0);
    }
}

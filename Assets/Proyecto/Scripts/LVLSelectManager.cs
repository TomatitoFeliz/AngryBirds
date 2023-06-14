using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVLSelectManager : MonoBehaviour
{
    public int lvl;
    public void LVLSelection()
    {
        SceneManager.LoadScene("NVL-0" + lvl.ToString());
    }
}

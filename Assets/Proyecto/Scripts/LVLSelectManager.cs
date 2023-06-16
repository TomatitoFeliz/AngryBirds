using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LVLSelectManager : MonoBehaviour
{
    [SerializeField]
    Button bLvl2, bLvl3;

    public int lvl;

    private void Start()
    {
        bLvl2.interactable = false;
        bLvl3.interactable = false;
    }
    public void LVLSelection()
    {
        SceneManager.LoadScene("NVL-0" + lvl.ToString());
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("NivelSuperado1") == 1)
        {
            bLvl2.interactable = true;
        }
        if (PlayerPrefs.GetInt("NivelSuperado2") == 1)
        {
            bLvl3.interactable = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public static SkinsManager Instance;

    public List<Material> skins;
    public Material playerSkin;
    public int chosenSkin = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SelectSkin();
        }
    }

    void SelectSkin()
    {
            if (chosenSkin == skins.Count - 1)
            {
                chosenSkin = 0;
            }
            else
            {
                chosenSkin += 1;
            }

            playerSkin = skins[chosenSkin];
    }
}

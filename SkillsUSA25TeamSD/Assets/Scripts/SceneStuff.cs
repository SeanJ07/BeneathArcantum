using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStuff : MonoBehaviour
{
    public Image sceneTransitioner;
    public Image SceneTransitionObject;
    public GameObject StartScreen;
    public GameObject Canvas;


    public SceneAsset sceneToTransition;


    private void Awake()
    {
        if (SceneTransitionObject == null)
        {
            SceneTransitionObject = Instantiate(sceneTransitioner, Canvas.transform);
            SceneTransitionObject.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            SceneTransitionObject.gameObject.SetActive(true);
        }
        if (StartScreen != null) { StartScreen.SetActive(true); } else { return; } //if startscreen is assigned in inspector, enable it, else just dont.
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting");
        StartCoroutine(SceneTransitioningOut());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SceneTransitioningIn() // Fading to black screen when transitioning to other scenes
    {
        SceneTransitionObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneTransitionObject.GetComponent<Animator>().Play("Fading");
    }

    public IEnumerator SceneTransitioningOut() // Fading out of black screen when entering a different scene
    {
        yield return new WaitForSeconds(0.5f);
        SceneTransitionObject.GetComponent<Animator>().Play("FadingOut");
        yield return new WaitForSeconds(2f);
        SceneTransitionObject.gameObject.SetActive(false);
    }

    public IEnumerator StartGameCoroutine()
    {
        StartCoroutine(SceneTransitioningIn());
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainGame");
    }

    public IEnumerator StartSandboxCoroutine()
    {
        StartCoroutine(SceneTransitioningIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SandBox");
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void StartSandbox()
    {
        StartCoroutine(StartSandboxCoroutine());
    }

    public IEnumerator GoToScene()
    {
        StartCoroutine(SceneTransitioningIn());
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToTransition.name);
    }

    public void SceneTransitioner()
    {
        StartCoroutine(GoToScene());
    }


}

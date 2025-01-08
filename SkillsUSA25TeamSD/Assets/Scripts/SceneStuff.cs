using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStuff : MonoBehaviour
{
    public Image sceneTransitioner; // The prefab object used to do smooth transitions from scene to scene.
    public Image SceneTransitionObject; // The prefab above assigned in this script.
    public GameObject StartScreen;
    public GameObject Canvas;


    public SceneAsset sceneToTransition;


    private void Awake()
    {
        if (SceneTransitionObject == null) // if there isnt a scene transition object (black screen that fades) in the scene, it makes one.
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

    public IEnumerator StartGameCoroutine() // Actually transitions to the game scene
    {
        StartCoroutine(SceneTransitioningIn());
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainGame");
    }

    public IEnumerator StartSandboxCoroutine() // Actually transitions to the sandbox scene
    {
        StartCoroutine(SceneTransitioningIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SandBox");
    }

    public void StartGame() // Put into a void function so you can use it in unity buttons and events.
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void StartSandbox() // Put into a void function so you can use it in unity buttons and events.
    {
        StartCoroutine(StartSandboxCoroutine());
    }

    public IEnumerator GoToScene() // Transitions to any other scene, should use this but haven't gotten the chance to change the code.
    {
        StartCoroutine(SceneTransitioningIn());
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToTransition.name);
    }

    public void SceneTransitioner() // Put into a void function so you can use it in unity buttons and events.
    {
        StartCoroutine(GoToScene());
    }


}

using UnityEngine;
using Unity.VisualScripting;
public class JustPlayAnim : MonoBehaviour
{
    [Tooltip("Assign the target GameObject with an Animation component")]
    public GameObject targetObject;

    [Tooltip("Specify the animation clip name")]
    public string animationName;

    private Animation anim;

    void Awake()
    {
        if (targetObject != null)
        {
            anim = targetObject.GetComponent<Animation>();
            if (anim == null)
            {
                Debug.LogError("No Animation component found on the target object!");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned!");
        }
    }

    // Public method to play the animation
    public void PlayAnimation()
    {
        if (anim != null && anim[animationName] != null)
        {
            anim.Play(animationName);
        }
        else
        {
            Debug.LogError($"Animation '{animationName}' not found or Animation component is missing!");
        }
    }

    // Public method to set the animation name at runtime
    public void SetAnimationName(string name)
    {
        animationName = name;
    }

    // Public method to set the target object at runtime
    public void SetTargetObject(GameObject obj)
    {
        targetObject = obj;
        anim = targetObject.GetComponent<Animation>();
        if (anim == null)
        {
            Debug.LogError("No Animation component found on the new target object!");
        }
    }
}

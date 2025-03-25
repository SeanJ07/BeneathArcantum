using UnityEngine;
using UnityEngine.EventSystems;

public class SelectUIButton : MonoBehaviour
{
    // Call this method to select the currently selected button (or any UI element)
    public void SelectCurrentSelection()
    {
        // Check if there is an active EventSystem
        if (EventSystem.current != null)
        {
            // Select the first UI element in the EventSystem
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
    }
}

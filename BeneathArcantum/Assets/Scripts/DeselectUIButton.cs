using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectUIButton : MonoBehaviour
{
    // Call this method to deselect the currently selected button (or any UI element)
    public void DeselectCurrentSelection()
    {
        // Check if there is an active EventSystem
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MatchWires : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler,IPointerUpHandler
{
    static MatchWires hoverWire;
    
    public GameObject lineObject;
    
    public string wireName;

    private GameObject line;
    
    public void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //create a line/wire when clicked on
        line = Instantiate(lineObject, transform.position, Quaternion.identity, transform.parent.parent);

        //get position of point of the click
        UpdateWire(Input.mousePosition);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //updates position of wire
        UpdateWire(Input.mousePosition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverWire = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!this.Equals(hoverWire) && wireName.Equals(hoverWire.wireName))
        {
            UpdateWire(hoverWire.transform.position);
            MatchWiresLogic.AddPoint();
            Destroy(hoverWire);
            Destroy(this);
            
        }
        else
        {
            Destroy(line);
        }
    }

    void UpdateWire(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        line.transform.right = direction;

        line.transform.localScale = new Vector3(direction.magnitude/150, 1, 1);
    }
    // Start is called before the first frame update
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MatchWires : MonoBehaviour
{
    static MatchWires hoverWire;
    
    public GameObject lineObject;
    
    public string wireName;

    private GameObject line;
    private LineRenderer lineRend;


    
    public void Start()
    {
        
    }

    public void OnMouseDown(PointerEventData eventData)
    {
        //create a line/wire when clicked on
        line = Instantiate(lineObject, transform.position, Quaternion.identity, transform.parent.parent);
        lineRend = line.GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, transform.position);
        //get position of point of the click
        UpdateWire(Input.mousePosition);
    }
    public void OnMouseDrag(PointerEventData eventData)
    {
        //updates position of wire
        UpdateWire(Input.mousePosition);
    }

    public void OnMouseEnter(PointerEventData eventData)
    {
        hoverWire = this;
    }

    public void OnMouseUp(PointerEventData eventData)
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

        lineRend.SetPosition(1, position);
    }
    // Start is called before the first frame update
    
}

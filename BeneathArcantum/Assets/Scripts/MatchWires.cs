using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MatchWires : MonoBehaviour //IPointerDownHandler, IDragHandler, IPointerEnterHandler,IPointerUpHandler
{
    static MatchWires hoverWire;
    
    public GameObject lineObject;
    
    public string wireName;

    private Camera uiCamera;
    private GameObject line;
    private LineRenderer lineRend;


    
    public void Start()
    {
        uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }

    private void Update()
    {
        OnMouseDrag();
    }
    public void OnMouseDown()
    {
        Vector3 mousePos = uiCamera.ScreenToWorldPoint(Input.mousePosition);
        //create a line/wire when clicked on
        line = Instantiate(lineObject, transform.position, Quaternion.identity, transform.parent.parent);
        lineRend = line.GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, transform.position);
        
        //get position of point of the click
        UpdateWire(mousePos);
    }
    public void OnMouseDrag()
    {
        //updates position of wire
        Vector3 mousePos = uiCamera.ScreenToWorldPoint(Input.mousePosition);
      
        
        UpdateWire(mousePos);
    }

    public void OnMouseEnter()
    {
        hoverWire = this;
    }

    public void OnMouseUp()
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

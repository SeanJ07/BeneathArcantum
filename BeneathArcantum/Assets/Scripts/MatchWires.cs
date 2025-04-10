using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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

   
    public void OnMouseDown()
    {
        Vector3 startMousePos = uiCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, uiCamera.transform.position.z));
        //create a line/wire when clicked on
        line = Instantiate(lineObject, transform.position, Quaternion.identity, transform.parent.parent);
        
        lineRend = line.GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, startMousePos.z));
        
        
        //get position of point of the click
        
    }
    public void OnMouseDrag()
    {
        //updates position of wire
        Vector3 mousePos = uiCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, uiCamera.transform.position.z));
      
        
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
        //Vector3 direction = position - transform.position;
        line.transform.right = position;
        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, new Vector3(position.x, position.y, position.z));
    }


}

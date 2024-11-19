using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement_Card : MonoBehaviour , IDragHandler , IEndDragHandler
{
    public Camera Camera;

    public void start()
    {
        Camera = GetComponent<Camera>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        Vector2 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = PosMouse;
        Debug.Log("PRESA CARTA"); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = PosMouse;
        Debug.Log("PRESA CARTA");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

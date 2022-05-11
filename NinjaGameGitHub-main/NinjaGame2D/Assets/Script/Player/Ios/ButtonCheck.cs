using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCheck : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public bool Pressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Pressed = false;
    }
    
}

using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    public float speed = 5f;
    
    void Update()
    {
        var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mp, 
            Time.deltaTime * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputEvent
{
    None = 0,
    TouchRelease = 10,
}

public class InputManager : MonoBehaviour {
    /// <summary>
    /// Singleton of the InputManager
    /// </summary>
    public static InputManager Instance { get; private set; }

    /// <summary>
    /// Event dispatcher
    /// </summary>
    public Dispatcher<InputEvent> dispatcher = new Dispatcher<InputEvent>();

    /// <summary>
    /// Touch offset relative to touch start position, normalized according to Screen size.
    /// </summary>
    public Vector2 touchOffset;

    /// <summary>
    /// Touch offset in pixels
    /// </summary>
    public Vector2 touchOffsetPixels;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private Vector3 touchStartPos;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }


        if (Input.GetMouseButtonUp(0))
        {
            dispatcher.Dispatch(InputEvent.TouchRelease);
        }


        if (Input.GetMouseButton(0))
        {
            touchOffsetPixels = Input.mousePosition - touchStartPos;

            touchOffset = touchOffsetPixels;
            touchOffset.x /= Screen.width;
            touchOffset.y /= Screen.height;
        }else
        {
            touchOffsetPixels = Vector2.zero;
            touchOffset = Vector2.zero;

        }

    }

    
}

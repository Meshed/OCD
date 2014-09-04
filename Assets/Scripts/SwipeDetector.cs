using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{
    public float minSwipeDistY;
    public float minSwipeDistX;
    private Vector2 startPos;

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public delegate void SwipeHandler(SwipeDirection swipeDirection);
    public static event SwipeHandler OnDotSwipe;

    void Update()
	{
//#if UNITY_ANDROID
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches[0];
			
			switch (touch.phase) 
			{
			    case TouchPhase.Began:
				    startPos = touch.position;
				    break;
			    case TouchPhase.Ended:
					    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

					    if (swipeDistVertical > minSwipeDistY) 
					    {
						    float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					        if (swipeValue > 0) //up swipe
					        {
                                Debug.Log("Swipe up");
                                OnDotSwipe(SwipeDirection.Up);
					        }
					        else if (swipeValue < 0) //down swipe
					        {
                                Debug.Log("Swipe down");
                                OnDotSwipe(SwipeDirection.Down);
					        }
					    }
					
					    float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
					
					    if (swipeDistHorizontal > minSwipeDistX) 
					    {
						    float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

					        if (swipeValue > 0) //right swipe
					        {
                                Debug.Log("Swipe right");
					            OnDotSwipe(SwipeDirection.Right);
					        }
					        else if (swipeValue < 0) //left swipe
					        {
                                Debug.Log("Swipe left");
                                OnDotSwipe(SwipeDirection.Left);
					        }
					    }
				    break;
			}
		}
	}
}
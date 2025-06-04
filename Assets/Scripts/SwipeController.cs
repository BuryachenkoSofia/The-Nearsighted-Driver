using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private float minSwipeDistance = 40f;
    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            endTouchPosition = Input.GetTouch(0).position;
            if (endTouchPosition.x < Screen.width / 2) player.MoveToPreviousLane();
            else player.MoveToNextLane();

        }
    }
}

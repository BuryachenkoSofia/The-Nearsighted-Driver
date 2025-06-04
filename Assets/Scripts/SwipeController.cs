using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Input.GetTouch(0).position.x < Screen.width / 2) player.MoveToPreviousLane();
            else player.MoveToNextLane();
        }
    }
}
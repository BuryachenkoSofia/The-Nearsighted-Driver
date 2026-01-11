using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Player player;
    private bool touchController;
    private float slipChance = 0.1f;
    private float inputTimer = 0f;

    private void Start()
    {
        touchController = true;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        inputTimer -= Time.deltaTime;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && touchController)
        {
            bool moved = false;
            if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                player.MoveToPreviousLane();
                moved = true;
            }
            else
            {
                player.MoveToNextLane();
                moved = true;
            }
            player.Slip(moved);
        }
    }
}
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private Ball _ball;
    
    private float _widthScreen;
    private float _heightScreen;

    private void Awake()
    {
        _heightScreen = Screen.height;
        _widthScreen = Screen.width;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _ball.IsReady)
        {
            var mousePos = Input.mousePosition;
            var mousePosY = mousePos.y / _heightScreen;

            if (mousePosY < 0.2f)
            {
                var mouseX = mousePos.x / _widthScreen;
                _trajectory.UpdateTrajectory(mouseX);
            }

            _trajectory.ShowTrajectory();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _trajectory.RemoveTrajectory();
            StartCoroutine(_ball.Move(_trajectory));
        }
    }
}

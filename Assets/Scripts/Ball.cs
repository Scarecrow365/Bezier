using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float _time;

    public bool IsReady { get; private set; } = true;

    public IEnumerator Move(Trajectory trajectory)
    {
        while (_time <= 1)
        {
            IsReady = false;
            transform.position = trajectory.GetTrajectory(_time);
            yield return new WaitForEndOfFrame();
            _time += Time.deltaTime;
        }

        yield return new WaitForSeconds(1);
        _time = 0;
        transform.position = Vector3.zero;
        IsReady = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
        }
    }
}

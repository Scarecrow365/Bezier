using UnityEditor;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private BezierStruct _bezierInfo;
    [SerializeField] private float _bounds = 5f;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
    }

    public void ShowTrajectory()
    {
        var points = new Vector3[50];
        _lineRenderer.positionCount = points.Length;

        for (var i = 0; i < points.Length; i++)
        {
            var expectedTime = (float)i / points.Length;
            points[i] = Bezier.GetBezierPoint(_bezierInfo, expectedTime);
        }

        _lineRenderer.SetPositions(points);
    }

    public void UpdateTrajectory(float t)
    {
        var pos1 = _bezierInfo.p1.position;
        var pos2 = _bezierInfo.p2.position;

        const float quarter = 4f; // 0.25f = 1/4

        if (t > 0.75f)
        {
            var newT = Mathf.Lerp(0, quarter, t - 0.75f);
            MoveRightSide(ref _bezierInfo.p2, pos2, newT, true);

            return;
        }

        if (t > 0.5)
        {
            var newT = Mathf.Lerp(0, quarter, t - 0.5f);

            MoveLeftSide(ref _bezierInfo.p1, pos1, newT,false);
            MoveLeftSide(ref _bezierInfo.p2, pos2, newT,false);

            return;
        }

        if (t > 0.25f)
        {
            var newT = Mathf.Lerp(0, quarter, t - 0.25f);

            MoveRightSide(ref _bezierInfo.p1, pos1, newT,false);
            MoveRightSide(ref _bezierInfo.p2, pos2, newT,false);

            return;
        }

        if (t > 0f)
        {
            var newT = Mathf.Lerp(0, quarter, t);
            MoveLeftSide(ref _bezierInfo.p2, pos2, newT, true);
        }
    }

    private void MoveLeftSide(ref Transform bezierPoint, Vector3 posPoint, float newT, bool isInverse)
    {
        var posX = isInverse ? -_bounds : _bounds;

        bezierPoint.position = Vector3.Lerp(
            new Vector3(0, posPoint.y, posPoint.z),
            new Vector3(posX, posPoint.y, posPoint.z),
            newT);
    }

    private void MoveRightSide(ref Transform bezierPoint, Vector3 posPoint, float newT, bool isInverse)
    {
        var posX = isInverse ? _bounds : -_bounds;

        bezierPoint.position = Vector3.Lerp(
            new Vector3(posX, posPoint.y, posPoint.z),
            new Vector3(0, posPoint.y, posPoint.z),
            newT);
    }

    public Vector3 GetTrajectory(float t) => Bezier.GetBezierPoint(_bezierInfo, t);
    public void RemoveTrajectory() => _lineRenderer.positionCount = 0;


    public void OnDrawGizmos()
    {
        Handles.DrawBezier(
            _bezierInfo.p0.position,
            _bezierInfo.p2.position,
            _bezierInfo.p1.position,
            _bezierInfo.p2.position,
            Color.white,
            EditorGUIUtility.whiteTexture, 1f);
    }
}

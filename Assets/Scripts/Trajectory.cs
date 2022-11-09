using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    [SerializeField] private int trajectoryLength = 20;
    private LineRenderer _lineRenderer;
    Vector3[] _points;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _points = new Vector3[trajectoryLength];
        _lineRenderer.positionCount = _points.Length;

        _lineRenderer.startColor = Color.clear;
        _lineRenderer.endColor = Color.clear;
    }

    public void HideTrajectory()
    {
        _lineRenderer.startColor = Color.clear;
        _lineRenderer.endColor = Color.clear;
    }

    public void ShowTrajectory(Vector2 origin, Vector2 speed)
    {
        _lineRenderer.startColor = speed.magnitude < 3 ? Color.clear : Color.clear + Color.white * speed.magnitude / 5;
        _lineRenderer.endColor = speed.magnitude < 3 ?Color.clear : Color.clear + Color.white* speed.magnitude / 5;
        
        for (int i = 0; i < _points.Length; i++)
        {
            float time = i * .1f;

            _points[i] = origin + speed * time + Physics2D.gravity * time * time / 2f;
        }
        
        _lineRenderer.SetPositions(_points);
    }
}

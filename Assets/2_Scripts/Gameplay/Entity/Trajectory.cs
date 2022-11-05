using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private GameObject dotParent;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int dotsNumber;
    [SerializeField] private float dotSpacing;
    [SerializeField] private float dotMinScale;

    private Transform[] dots;
    private Vector3 position;
    private float timeStamp;

    private void Start()
    {
        Hide();

        PrepareDots();
    }

    private void PrepareDots()
    {
        dots = new Transform[dotsNumber];

        float currentScale = 1f;
        float scaleFactor = currentScale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dots[i] = Instantiate(dotPrefab, dotParent.transform).transform;
            dots[i].transform.localScale = Vector3.one * currentScale;

            if (currentScale > dotMinScale)
            {
                currentScale -= scaleFactor;
            }
        }
    }

    public void Show()
    {
        dotParent.SetActive(true);
    }

    public void Hide()
    {
        dotParent.SetActive(false);
    }

    public void UpdateDots(Vector3 ballPosition, Vector2 force)
    {
        timeStamp = dotSpacing;

        for (int i = 0; i < dotsNumber; i++)
        {
            /* Công thức ném xiên
             * x = x0 + F * t
             * y = y0 + F * t - (g * t^2) / 2
             */
            position.x = ballPosition.x + force.x * timeStamp;
            position.y = ballPosition.y + force.y * timeStamp - Ball.GravityScale * Physics2D.gravity.magnitude * Mathf.Pow(timeStamp, 2) / 2f;
            dots[i].position = position;

            timeStamp += dotSpacing;
        }
    }
}

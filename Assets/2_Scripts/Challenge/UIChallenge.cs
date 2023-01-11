using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallenge : MonoBehaviour
{
    #region ASSET
    [Header("Color")]
    [SerializeField] private Color _orange;
    [SerializeField] private Color _turquoise;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _green;
    [SerializeField] private Color _violet;
    [SerializeField] private Color _red;

    [Header("Icon")]
    [SerializeField] private GameObject _ballIcon;
    [SerializeField] private GameObject _tokenIcon;
    [SerializeField] private GameObject _clockIcon;
    [SerializeField] private GameObject _scoreIcon;
    [SerializeField] private GameObject _bounceIcon;
    #endregion

    [Header("Component")]
    [SerializeField] private Image _topArea;
    [SerializeField] private TextMeshProUGUI _challengeName;
    [SerializeField] private TextMeshProUGUI _basketAmount;

    private void OnEnable()
    {
        Observer.OnPlayChallenge += LoadUIChallenge;
    }

    private void OnDisable()
    {
        Observer.OnPlayChallenge -= LoadUIChallenge;
    }

    private void LoadUIChallenge(Challenge challenge)
    {

    }

    public void OnCancelButton()
    {
        Debug.Log("CANCEL");
    }

    public void OnPlayButton()
    {
        Debug.Log("PLAY");
    }

    public void OnPauseButton()
    {
        Debug.Log("PAUSE");
    }

    public void OnDrawGizmos()
    {
        Debug.Log("GIVE UP");
    }
}
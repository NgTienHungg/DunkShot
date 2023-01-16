using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeSelection : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _progress;
    [SerializeField] private ChallengeType _type;

    private int _currentLevel;
    private int _numberOfLevels;

    private void Awake()
    {
        foreach (Challenge challenge in ChallengeManager.Instance.Challenges)
        {
            if (challenge.Type == _type)
            {
                _numberOfLevels += 1;
            }
        }
    }

    private void OnEnable()
    {
        _currentLevel = ChallengeManager.Instance.GetCurrentLevelOfChallenge(_type);

        _fill.fillAmount = 1f * (_currentLevel - 1) / _numberOfLevels;
        _progress.text = (100 * (_currentLevel - 1) / _numberOfLevels) + "%";

        if (_currentLevel > _numberOfLevels)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void OnClick()
    {
        ChallengeManager.Instance.SetCurrentChallenge(_type);
        CanvasController.Instance.UIChallenge.LoadChallenge();
    }
}
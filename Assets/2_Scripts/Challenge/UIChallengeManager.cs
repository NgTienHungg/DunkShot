using UnityEngine;

public class UIChallengeManager : UIGame
{
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void OnBackButton()
    {
        CanvasController.Instance.CloseChallenge();
    }
}
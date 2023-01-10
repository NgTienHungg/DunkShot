using UnityEngine;

public class ChallengeManager : UIGame
{
    public void OnBackButton()
    {
        UIManager.Instance.CloseChallenge();
    }
}
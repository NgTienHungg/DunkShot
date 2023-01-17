using System;
using UnityEngine;
using System.Collections.Generic;

public class Achievement : MonoBehaviour
{
    public static Achievement Instance { get; private set; }
    private List<Skin> _listMissionSkin;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        ManageMissionSkin();
        RegisterListener();
    }

    private void ManageMissionSkin()
    {
        _listMissionSkin = new List<Skin>();

        foreach (var skin in DataManager.Instance.Skins)
        {
            if (skin.Data.Type == SkinType.Mission)
            {
                _listMissionSkin.Add(skin);
            }
        }
    }

    private void RegisterListener()
    {
        Observer.NewBestScore += NewBestScore;
        Observer.Perfect += Perfect;
        Observer.ContinueGame += ContinueGame;
        Observer.PointScored += PointScored;
        Observer.Bounce += Bounce;
        Observer.CollectStar += CollectStar;
        Observer.PlayGame += PlayGame;
        Observer.MultiBounce += MuiltiBounce;
    }

    private void NewBestScore()
    {
        if (!_listMissionSkin[0].Unlocked)
        {
            _listMissionSkin[0].NewMilestone(ScoreManager.Instance.BestScore);
        }
    }

    private void Perfect()
    {
        if (!_listMissionSkin[1].Unlocked)
        {
            _listMissionSkin[1].NewMilestone(ScoreManager.Instance.Perfect);
        }
    }

    private void ContinueGame()
    {
        if (!_listMissionSkin[2].Unlocked)
        {
            _listMissionSkin[2].NewProgress(1);
        }
    }

    private void PointScored()
    {
        if (!_listMissionSkin[3].Unlocked)
        {
            _listMissionSkin[3].NewProgress(ScoreManager.Instance.ScoreAdd);
        }
    }

    private void Bounce()
    {
        if (!_listMissionSkin[4].Unlocked)
        {
            _listMissionSkin[4].NewProgress(1);
        }
    }

    private void CollectStar()
    {
        if(!_listMissionSkin[5].Unlocked)
        {
            _listMissionSkin[5].NewProgress(1);
        }
    }

    private void PlayGame()
    {
        if (!_listMissionSkin[6].Unlocked)
        {
            _listMissionSkin[6].NewProgress(1);
        }
    }

    private void MuiltiBounce()
    {
        if (!_listMissionSkin[7].Unlocked)
        {
            _listMissionSkin[7].NewProgress(1);
        }
    }
}
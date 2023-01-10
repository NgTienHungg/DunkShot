using UnityEngine;

public class Skin : MonoBehaviour
{
    public SkinData Data { get; private set; }
    public int ID { get; private set; }
    public string Key { get; private set; }
    public bool Unlocked { get; private set; }
    public int VideoWatched { get; private set; }
    public int MissionProgress { get; private set; }

    private readonly string UNLOCKED = "Unlocked";
    private readonly string VIDEO_WATCHED = "VideoWatched";
    private readonly string MISSION_PROGRESS = "MissionProgress";

    public void SetData(SkinData data)
    {
        Data = data;
        ID = int.Parse(data.name); // ex: ID = 1
        Key = Data.Type.ToString() + ID.ToString("00"); // ex: Key = TradingBall01

        Unlocked = SaveSystem.GetInt(UNLOCKED + Key) == 1 ? true : false;

        if (Data.Type == SkinType.Video)
            VideoWatched = SaveSystem.GetInt(VIDEO_WATCHED + Key);

        if (Data.Type == SkinType.Mission)
            MissionProgress = SaveSystem.GetInt(MISSION_PROGRESS + Key);
    }

    public void Unlock()
    {
        Unlocked = true;
        SaveSystem.SetInt(UNLOCKED + Key, 1);
        Observer.OnUnlockSkin?.Invoke(this);
    }

    public void Select()
    {
        SaveSystem.SetString(SaveKey.SKIN_IN_USE, Key);
    }

    public void WatchVideo()
    {
        if (VideoWatched == Data.NumberOfVideos)
            return;

        VideoWatched++;
        SaveSystem.SetInt(VIDEO_WATCHED + Key, VideoWatched);
    }
}
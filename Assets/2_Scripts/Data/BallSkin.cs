using UnityEngine;

public class BallSkin : MonoBehaviour
{
    public BallSkinData Data { get; private set; }
    public int ID { get; private set; }
    public string Name { get; private set; }
    public bool Unlocked { get; private set; }
    public int VideoWatched { get; private set; }
    public int MissionProgress { get; private set; }

    // Key save
    private static readonly string UNLOCKED = "Unlocked";
    private static readonly string VIDEO_WATCHED = "VideoWatched";
    private static readonly string MISSION_PROGRESS = "MissionProgress";

    public void SetData(BallSkinData data, int id)
    {
        Data = data;
        ID = int.Parse(data.name);
        Name = Data.Type.ToString() + ID.ToString("00"); // EX: _name = TradingBall01

        Unlocked = SaveSystem.GetInt(UNLOCKED + Name) == 1 ? true : false;

        if (Data.Type == SkinType.VideoBall)
            VideoWatched = SaveSystem.GetInt(VIDEO_WATCHED + Name);
        
        if (Data.Type == SkinType.MissionBall)
            MissionProgress = SaveSystem.GetInt(MISSION_PROGRESS + Name);
    }

    public void Unlock()
    {
        SaveSystem.SetInt(UNLOCKED + name, 1);
        Unlocked = true;
    }

    public void WatchVideo()
    {
        if (VideoWatched == Data.NumberOfVideos)
            return;

        VideoWatched++;
        SaveSystem.SetInt(VIDEO_WATCHED + Name, VideoWatched);
    }
}
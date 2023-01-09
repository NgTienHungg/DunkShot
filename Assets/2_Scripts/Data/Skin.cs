using UnityEngine;

public class Skin : MonoBehaviour
{
    public SkinData Data { get; private set; }
    public int ID { get; private set; }
    public string Name { get; private set; }
    public bool Unlocked { get; private set; }
    public int VideoWatched { get; private set; }
    public int MissionProgress { get; private set; }

    // Key save
    private static readonly string UNLOCKED = "Unlocked";
    private static readonly string VIDEO_WATCHED = "VideoWatched";
    private static readonly string MISSION_PROGRESS = "MissionProgress";

    public void SetData(SkinData data)
    {
        Data = data;
        ID = int.Parse(data.name); // ex: ID = 1
        Name = Data.Type.ToString() + ID.ToString("00"); // ex: Name = TradingBall01

        Unlocked = SaveSystem.GetInt(UNLOCKED + Name) == 1 ? true : false;

        if (Data.Type == SkinType.Video)
            VideoWatched = SaveSystem.GetInt(VIDEO_WATCHED + Name);
        
        if (Data.Type == SkinType.Mission)
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
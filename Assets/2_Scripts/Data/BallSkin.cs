using UnityEngine;

public class BallSkin : MonoBehaviour
{
    public BallSkinData Data { get; private set; }

    public string Name { get; private set; }

    public bool Unlocked { get; private set; }

    public int VideoWatched { get; private set; }

    // Key save
    private static readonly string UNLOCKED = "Unlocked";
    private static readonly string VIDEO_WATCHED = "VideoWatched";

    public void SetData(BallSkinData data, int id)
    {
        Data = data;
        Name = data.Type.ToString() + id.ToString("00"); // EX: _name = TradingBall01

        Unlocked = SaveSystem.GetInt(UNLOCKED + Name) == 1 ? true : false;

        if (Data.Type == SkinType.VideoBall)
        {
            VideoWatched = SaveSystem.GetInt(VIDEO_WATCHED + Name);
        }
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
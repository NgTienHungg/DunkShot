using UnityEngine;

public class BallSkin : MonoBehaviour
{
    private BallSkinData _data;
    public BallSkinData Data { get => _data; }

    private string _name;
    public string Name { get => _name; }

    private bool _unlocked;
    public bool Unlocked { get => _unlocked; }

    public void SetData(BallSkinData data, int id)
    {
        _data = data;
        _name = data.Type.ToString() + id.ToString("00"); // EX: _name = TradingBall01
        _unlocked = SaveSystem.GetInt("Unlocked" + _name) == 1 ? true : false;
    }

    public void Unlock()
    {
        SaveSystem.SetInt("Unlocked" + name, 1);
        _unlocked = true;
    }
}
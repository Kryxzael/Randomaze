using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SpawnrateGlider : MonoBehaviour
{    
    private Slider _slider;

    private Slider.SliderEvent _unhookedEv = new Slider.SliderEvent();
    private Slider.SliderEvent _hookedEv = new Slider.SliderEvent();

    private Tile _connectedTile;

    /// <summary>
    /// Gets or sets thet value of this spawnrate glider
    /// </summary>
    public float Spawnrate
    {
        get
        {
            return _slider.value;
        }
        set
        {
            _slider.value = Mathf.Clamp(value, 0f, 1f);
        }
    }

    /// <summary>
    /// The tile that this spawnrate glider represents
    /// </summary>
    public Tile ConnectedTile
    {
        get
        {
            return _connectedTile;
        }
        set
        {
            _connectedTile = value;
            transform.Find("TilePreview").GetComponent<Image>().sprite = 
                GameState.Settings.ResourceSet.GetResource<SpriteAnimation>(value.GetSpriteAnimationName()).Frames.First().Sprite;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _slider = GetComponentInChildren<Slider>();
        _hookedEv.AddListener(SendMyValue);
        _unhookedEv.AddListener(SetText);
        
    }

    private void Start()
    {
        SetText(Spawnrate);
    }

    /// <summary>
    /// Enables hooking, that is, updating the current generator whenever this slider changes
    /// </summary>
    public void Hook()
    {
        _slider.onValueChanged = _hookedEv;
    }

    /// <summary>
    /// Disables hooking, that is, not longer updating the current generator whenever this slider changes
    /// </summary>
    public void Unhook()
    {
        _slider.onValueChanged = _unhookedEv;
    }


    private void SendMyValue(float value)
    {
        if (GameState.Settings.Generator is GeneratorConfigurable)
        {
            (GameState.Settings.Generator as GeneratorConfigurable).SetSpawnrate(ConnectedTile.GetName(), Spawnrate);
            SetText(value);
        }
    }

    private void SetText(float spawnRate)
    {
        if (spawnRate == 0)
        {
            transform.Find("lblSelectPreset").GetComponent<Text>().text = ConnectedTile.GetName() + " - OFF";
        }
        else
        {
            transform.Find("lblSelectPreset").GetComponent<Text>().text = ConnectedTile.GetName() + " - " + (spawnRate * 100).ToString("##0") + "%";
        }
        
    }

}
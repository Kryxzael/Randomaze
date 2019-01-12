using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Difficulty sources")]
    public Generator GeneratorAscending;
    public Generator GeneratorEasy;
    public Generator GeneratorNormal;
    public Generator GeneratorHard;
    public Generator GeneratorExtreme;

    [Header("Presets")]
    public List<Generator> Presets;

    [Header("Prefabs")]
    public SpawnrateGlider SliderPrefab;

    private const float SLIDER_MARGIN = 150f;
    private const float PAGE_SLIDE_SPEED = 20f;
    private const int DEFAULT_PAGE_X = 820;
    private const int EXTENDED_PAGE_X = 0;

    /// <summary>
    /// Is the menu currently in an expanded view?
    /// </summary>
    public bool ExtendedView { get; private set; }

    private void Start()
    {
        //Loads the init scene first, if it has not been loaded
        if (!GameState.Initialized)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        //Create sliders
        {
            int i = -1;
            RectTransform parent = transform.Find("SlidingPanel/SettingPageCustom/Faders/Viewport/Content") as RectTransform;

            foreach (Tile t in Tile.AllTiles.Where(t => t.ShowInCustomInspector()))
            {
                //Don't ask
                SpawnrateGlider g = Instantiate(SliderPrefab, parent);
                g.transform.localPosition = new Vector2(0, i-- * SLIDER_MARGIN + 0.5f * SLIDER_MARGIN);
                g.ConnectedTile = t;
            }

            
        }


        //Create presets
        FindObjectOfType<Dropdown>().options = Presets
            .Select(i => new Dropdown.OptionData(i.GetName()))
            .ToList();

        //This will occur if this is the first time the menu is loaded
        if (GameState.Settings.Generator == null)
        {
            SetGenerator(GeneratorNormal);
        }
        
        ReloadFromGameState();
    }

    public void ReloadFromGameState()
    {
        Generator gen = GameState.Settings.Generator;


        //Load difficulty
        if (gen.Equals(GeneratorEasy))
        {
            GameObject.Find("DiffEasy").GetComponent<Toggle>().isOn = true;
        }
        else if (gen.Equals(GeneratorNormal))
        {
            GameObject.Find("DiffNormal").GetComponent<Toggle>().isOn = true;

        }
        else if (gen.Equals(GeneratorHard))
        {
            GameObject.Find("DiffHard").GetComponent<Toggle>().isOn = true;
        }
        else if (gen.Equals(GeneratorExtreme))
        {
            GameObject.Find("DiffExtreme").GetComponent<Toggle>().isOn = true;
        }
        else
        {
            GameObject.Find("DiffCustom").GetComponent<Toggle>().isOn = true;
        }

        

        //Update sliders
        SetGenerator(GameState.Settings.Generator);

        //Load gamemode settings
        GameObject.Find("OpKey").GetComponent<Toggle>().isOn = GameState.Settings.KeyMode;
        GameObject.Find("OpBlind").GetComponent<Toggle>().isOn = GameState.Settings.BlindMode;
        GameObject.Find("OpCoin").GetComponent<Toggle>().isOn = GameState.Settings.CoinMode;
        GameObject.Find("OpRandom").GetComponent<Toggle>().isOn = GameState.Settings.RandomizedGamemode;
    }

    /// <summary>
    /// Event Handler: Fired when any of the difficulties are selected
    /// </summary>
    /// <param name="sender"></param>
    public void EvDifficultyClick(GameObject sender)
    {
        switch (sender.name)
        {
            case "DiffAscending":
                SetGenerator(GeneratorAscending);
                break;
            case "DiffEasy":
                SetGenerator(GeneratorEasy);
                break;
            case "DiffNormal":
                SetGenerator(GeneratorNormal);
                break;
            case "DiffHard":
                SetGenerator(GeneratorHard);
                break;
            case "DiffExtreme":
                SetGenerator(GeneratorExtreme);
                break;
            case "DiffCustom":
                if (sender.GetComponent<Toggle>().isOn)
                {
                    ExtendedView = true;
                    StartCoroutine("CoNextPage");
                }
                break;
        }
    }

    /// <summary>
    /// Event handler: Fired when a preset is selected from the preset list
    /// </summary>
    /// <param name="sender"></param>
    public void EvPresetSelected(GameObject sender)
    {
        int v = sender.GetComponent<Dropdown>().value;
        SetGenerator(Presets[v]);
    }

    /// <summary>
    /// Event handler: Fired when the back button is clicked
    /// </summary>
    public void EvBackClicked()
    {
        ExtendedView = false;
        StartCoroutine("CoNextPage");
    }

    /// <summary>
    /// Event handler: Fired when the start button is clicked
    /// </summary>
    public void EvOnStartClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Event handler: Fired when a gamemode option is changed
    /// </summary>
    public void EvGamemodeOptionClicked(GameObject sender)
    {
        bool @checked = sender.GetComponent<Toggle>().isOn;
        switch (sender.name)
        {
            case "OpKey":
                GameState.Settings.KeyMode = @checked;
                break;
            case "OpBlind":
                GameState.Settings.BlindMode = @checked;
                break;
            case "OpCoin":
                GameState.Settings.CoinMode = @checked;
                break;
            case "OpRandom":
                GameState.Settings.RandomizedGamemode = @checked;
                break;
           
        }
    }

    /// <summary>
    /// Sets the generator of the game to the generator g and updates the gliders
    /// </summary>
    /// <param name="g">Generator to update to</param>
    private void SetGenerator(Generator g)
    {
        GameState.Settings.Generator = g.Clone();

        //Updates all the gliders
        FindObjectsOfType<SpawnrateGlider>().ForEach(i =>
        {
            i.Unhook();
            if (g is GeneratorConfigurable)
            {
                i.Spawnrate = (g as GeneratorConfigurable).GetSpawnrate(i.ConnectedTile.GetName());
            }
            i.Hook();
        });
    }

    /// <summary>
    /// Coroutine used to make the page go all cool slidey and shit
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoNextPage()
    {
        Transform trans = transform.Find("SlidingPanel");

        while (true)
        {
            //Moving to the right
            if (ExtendedView)
            {
                //Moves the page
                trans.Translate(-PAGE_SLIDE_SPEED, 0);

                //Breaks loop and error-corrects if destination has been reached
                if (trans.localPosition.x <= EXTENDED_PAGE_X)
                {
                    trans.localPosition = trans.localPosition.SetX(EXTENDED_PAGE_X);
                    break;
                }
            }
            //Moving to the left
            else
            {
                //Moves page
                trans.Translate(PAGE_SLIDE_SPEED, 0);

                //Breaks loop and error-corrects if destination has been reached
                if (trans.localPosition.x >= DEFAULT_PAGE_X)
                {
                    trans.localPosition = trans.localPosition.SetX(DEFAULT_PAGE_X);
                    break;
                }
                
            }

            yield return new WaitForFixedUpdate();
        }
    }
}


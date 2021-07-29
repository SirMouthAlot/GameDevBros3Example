using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject _activeMenu;
    public AudioSource _backgroundAudio;

    public List<KeyCode> _increaseVert;
    public List<KeyCode> _decreaseVert;
    public List<KeyCode> _increaseHoriz;
    public List<KeyCode> _decreaseHoriz;
    public List<KeyCode> _confirmButtons;

    private MenuDefinition _activeMenuDefinition;
    private int _activeButton = 0;

    public void Start()
    {
        //Update our active menu definition at the start to make sure it's set properly
        UpdateActiveMenuDefinition();
    }

    public void Update()
    {
        switch (_activeMenuDefinition.GetMenuType())
        {
            case MenuType.HORIZONTAL:
                MenuInput(_increaseVert, _decreaseVert, _confirmButtons);
                break;
            case MenuType.VERTICAL:
                MenuInput(_increaseHoriz, _decreaseHoriz, _confirmButtons);
                break;
        }
    }

    private void MenuInput(List<KeyCode> increase, List<KeyCode> decrease, List<KeyCode> confirm)
    {
        int newActive = _activeButton;

        for (int i = 0; i < increase.Count; i++)
        {
            if (Input.GetKeyDown(increase[i]))
            {
                newActive = SwitchCurrentButton(1);
            }
        }
        
        for (int i = 0; i < decrease.Count; i++)
        {
            if (Input.GetKeyDown(decrease[i]))
            {
                newActive = SwitchCurrentButton(-1);
            }
        }
        
        for (int i = 0; i < confirm.Count; i++)
        {
            if (Input.GetKeyDown(confirm[i]))
            {
                ClickCurrentButton();
            }
        }
        

        _activeButton = newActive;
    }

    private int SwitchCurrentButton(int increment)
    {
        int newActive = Utility.WrapAround(_activeMenuDefinition.GetButtonCount(), _activeButton, increment);

        _activeMenuDefinition.GetButtonDefinitions()[_activeButton].SwappedOff();
        _activeMenuDefinition.GetButtonDefinitions()[newActive].SwappedTo();

        return newActive;
    }

    private void ClickCurrentButton()
    {
        StartCoroutine(_activeMenuDefinition.GetButtonDefinitions()[_activeButton].ClickButton());
    }

    public void UpdateActiveMenuDefinition()
    {
         //Grab out our menu definition from the active menu
        _activeMenuDefinition = _activeMenu.GetComponent<MenuDefinition>();

        //Check if old active menu wanted us to continue music from old menu
        bool continueFromOldMusic = _activeMenuDefinition._continuePrevMusic;

        if (_activeMenuDefinition._menuMusic != null)
        {
            _backgroundAudio.clip = _activeMenuDefinition._menuMusic;
            _backgroundAudio.Play();
        }
        else if (!continueFromOldMusic)
        {
            _backgroundAudio.Stop();
        }
    }

    public void SetActiveMenu(GameObject activeMenu)
    {
        //Set our active menu
        _activeMenu = activeMenu;

        //Make sure to update our menu definition
        UpdateActiveMenuDefinition();
    }
}

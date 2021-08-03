using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuType
{
    HORIZONTAL,
    VERTICAL
}

public class MenuDefinition : MonoBehaviour
{
    public MenuType _menuType = MenuType.HORIZONTAL;
    public AudioClip _menuMusic;
    public bool _continuePrevMusic = false;
    public List<GameObject> _menuButtonObjects = new List<GameObject>();
    private List<ButtonDefinition> _menuButtonDefinitions = new List<ButtonDefinition>();
    private List<Button> _menuButtons = new List<Button>();
    private List<Animator> _menuAnimators = new List<Animator>();
    public List<GameObject> _numberDisplayObjects = new List<GameObject>();
    private List<NumberDisplayDefinition> _numberDisplayDefinitions = new List<NumberDisplayDefinition>();

    public void Start()
    {
        //Searches and grabs the components 
        for (int i = 0; i < _menuButtonObjects.Count; i++)
        {
            //Grab out our button defintion component
            _menuButtonDefinitions.Add(_menuButtonObjects[i].GetComponent<ButtonDefinition>());
            //Grab out our button component
            _menuButtons.Add(_menuButtonObjects[i].GetComponent<Button>());

            //Grab out our animator component if it's there
            Animator temp = null;
            _menuButtonObjects[i].TryGetComponent<Animator>(out temp);
            
            //If there is no animator it'll be null 
            //We'll check for null when using this value so we know not to use it if it's null
            _menuAnimators.Add(temp);
        }

        //Searches and grabs components
        for (int i = 0; i < _numberDisplayObjects.Count; i++)
        {
            //Grab out number display definitions
            _numberDisplayDefinitions.Add(_numberDisplayObjects[i].GetComponent<NumberDisplayDefinition>());
        }

    }

    public MenuType GetMenuType()
    {
        return _menuType;
    }

    public int GetButtonCount()
    {
        return _menuButtons.Count;
    }

    public List<ButtonDefinition> GetButtonDefinitions()
    {
        return _menuButtonDefinitions;
    }

    public List<Button> GetButtons()
    {
        return _menuButtons;
    }

    public List<Animator> GetAnimators()
    {
        return _menuAnimators;
    }

    public List<GameObject> GetNumberDisplayObjects()
    {
        return _numberDisplayObjects;
    }

    public List<NumberDisplayDefinition> GetNumberDisplayDefinitions()
    {
        return _numberDisplayDefinitions;
    }
}

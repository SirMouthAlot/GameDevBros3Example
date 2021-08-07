using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDefinition : MonoBehaviour
{
    public bool _animated = false;
    public Color _unselectedTint = Color.grey;
    public Color _selectedTint = Color.white;
    public bool _selected = false;
    public AudioClip _swapToSFX;
    public AudioClip _confirmSFX;
    public float _confirmTime;
    private Button _button;
    private Image _image;
    private Animator _animator;

    public bool _disableControls = false;


    private void Start()
    {
        _button = GetComponent<Button>();

        _image = GetComponent<Image>();

        //Is this item animated
        _animated = TryGetComponent<Animator>(out _animator);

        if (!_animated)
        {
            if (_selected)
            {
                _image.color = _selectedTint;
            }
            else
            {
                _image.color = _unselectedTint;
            }
        }
    }

    public void SwappedTo()
    {
        //Selected button
        _selected = true;

        //If there's SFX for swapping buttons, play it
        if (_swapToSFX != null)
        {
            AudioSource.PlayClipAtPoint(_swapToSFX, Vector3.zero);
        }

        //If there's an animator update the selected bool in the animator
        if (_animator != null)
        {
            _animator.SetBool("Selected", _selected);
        }

        //If not animated, tint the button to show selected
        if (!_animated)
        {
            _image.color = _selectedTint;
        }
    }

    public void SwappedOff()
    {
        //Unselected button
        _selected = false;

        //If there's an animator, update the selected bool in the animator
        if (_animator != null)
        {
            _animator.SetBool("Selected", _selected);
        }

        //If not animated, tint the button to show selected
        if (!_animated)
        {
            _image.color = _unselectedTint;
        }
    }

    public IEnumerator ClickButton()
    {
        if (!_disableControls)
        {
            _disableControls = true;

            //If there's SFX for confirming button, play it
            if (_confirmSFX != null)
            {
                AudioSource.PlayClipAtPoint(_confirmSFX, Vector3.zero);
            }

            yield return new WaitForSeconds(_confirmTime);

            //Invoke button click
            _button.onClick.Invoke();

            _disableControls = false;
        }
    }
}

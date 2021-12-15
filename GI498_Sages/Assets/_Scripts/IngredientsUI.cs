using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsUI : MonoBehaviour
{
    public PlayerInput _playerInput;
    public GameObject orderPanel;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    void Start()
    {
        // _playerInput.UI.ClosePanel.started += ctx =>
        // {

        // };
        // _playerInput.UI.ClosePanel.canceled += ctx =>
        // {

        // };
    }

    void Update()
    {
        if (_playerInput.UI.OpenIngredientsPanel.triggered)
        {
            orderPanel.SetActive(true);
        }
    }
}

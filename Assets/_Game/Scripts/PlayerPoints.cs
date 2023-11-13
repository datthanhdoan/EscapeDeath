using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    private PlayerMoverment _playerMoverMent;
    public TextMeshProUGUI _textMesh;
    private void Start()
    {
        _playerMoverMent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoverment>();
    }
    private void Update()
    {
        _textMesh.text = _playerMoverMent.points.ToString();
    }
}

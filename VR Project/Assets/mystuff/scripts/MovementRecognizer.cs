using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.XR.Interaction;
using System.IO;

public class MovementRecognizer : MonoBehaviour
{
    //[SerializeField] private XRNode _inputSource;
    //[SerializeField] private InputHelpers.Button _inputButton;
    //[SerializeField] private float _inputThreshhold = 0.1f;
    public InputActionProperty pinchAnimationAction;

    [SerializeField] private Transform _movementSource;
    [SerializeField] private float _betweenPositionsThreshold=0.05f;

    [SerializeField] private bool _creationMode = true;
    [SerializeField] private string newGestureName;

    [SerializeField] private SpellHand _spellHand;
    [SerializeField] private StatSheet _playerStats;

    private bool _isMoving = false;
    private List<Vector3> _positionList= new List<Vector3>();
    private Camera cam;

    GameManager gameManager => GameManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(_inputSource), _inputButton, out bool isPressed, _inputThreshhold);
        bool isPressed = false;
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if (triggerValue > 0) { isPressed = true; }

        //start
        if(!_isMoving && isPressed)
        {
            if (_playerStats.CanAttack)
            StartMovement();
        }
        //end
        else if(_isMoving && !isPressed)
        {
            EndMovement();
        }
        //update
        else if(_isMoving && isPressed)
        {
            UpdateMovement();
        }
    }

    void StartMovement()
    {
        _isMoving= true;
        _positionList.Clear();
        _positionList.Add(_movementSource.position);
    }
    void EndMovement()
    {
        _isMoving= false;

        Point[] pointArray = new Point[_positionList.Count];

        for (int i = 0; i < _positionList.Count; i++)
        {
            Vector2 screenPoint = cam.WorldToScreenPoint(_positionList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }
        
        Gesture newGesture = new Gesture(pointArray);

        // Add new Gesture
        if (_creationMode)
        {
            newGesture.Name = newGestureName;
            gameManager.Gestures.Add(newGesture);

            Debug.Log($"You Created: {newGestureName} Gesture");

            string fileName = Path.Combine(Application.streamingAssetsPath, $"XML/spells/{newGestureName}.xml");
            GestureIO.WriteGesture(pointArray,newGestureName,fileName);
        }

        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, gameManager.Gestures.ToArray());
            Debug.Log($"You Drew:{result.GestureClass} |  Score: {result.Score}");
            if (_playerStats.CanAttack)
            {
                _spellHand.CastSpell(result.GestureClass, result.Score);
            }
        }
    }
    void UpdateMovement()
    {
        Vector3 lastPosition = _positionList[_positionList.Count-1];
        if (Vector3.Distance(_movementSource.position,lastPosition)> _betweenPositionsThreshold)
        _positionList.Add(_movementSource.position);
    }
}

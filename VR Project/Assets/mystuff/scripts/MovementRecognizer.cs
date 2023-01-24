using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.XR.Interaction;
using System.IO;
using System;

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

    private float _lrStartWidth;
    [SerializeField] private float _lrTime = 2;
    private float _lrCurrentTime;
    private LineRenderer _lr => GetComponent<LineRenderer>();

    private bool _isMoving = false;
    private List<Vector3> _positionList= new List<Vector3>();
    private Camera cam;

    GameManager gameManager => GameManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        _lrStartWidth = _lr.startWidth;
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

        if (!_isMoving)
        {
            _lrCurrentTime -= Time.deltaTime/ _lrTime;

            float w = Mathf.Lerp(0, _lrStartWidth, _lrCurrentTime);
            _lr.startWidth = w;
            _lr.endWidth = w;
        }
    }

    void StartMovement()
    {
        _lrCurrentTime = 1;
        _lr.startWidth = _lrStartWidth;
        _lr.endWidth = _lrStartWidth;
        _lr.colorGradient = new Gradient();
        _isMoving = true;
        _positionList.Clear();
        _lr.positionCount = 2;
        _lr.SetPosition(0,transform.position);
        _lr.SetPosition(1, transform.position);
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
                SOspell spell = _spellHand.CastSpell(result.GestureClass, result.Score);
                if (spell != null)
                {
                    _lr.colorGradient = spell.color;
                }
            }
        }
    }
    void UpdateMovement()
    {
        _lr.positionCount++;
        _lr.SetPosition(_lr.positionCount-1, transform.position);
        Vector3 lastPosition = _positionList[_positionList.Count-1];
        if (Vector3.Distance(_movementSource.position,lastPosition)> _betweenPositionsThreshold)
        _positionList.Add(_movementSource.position);
    }
}

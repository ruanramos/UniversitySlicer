using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipe : MonoBehaviour
{
    private Touch _myTouch;
    private Vector2 _startTouchPosition;
    private Vector2 _finalTouchPosition;
    private Vector2 _swipeDirection;

    private bool _shouldCallFunc;
    private float _timeStartedTouching;

    private void Start()
    {
        _shouldCallFunc = false;
        _swipeDirection = Vector2.zero;
    }

    // Update is called once per frame
    private void Update () {
        if (Input.touchCount <= 0) return;
        _myTouch = Input.GetTouch(0);
        switch (_myTouch.phase)
        {
            case TouchPhase.Began:
                _timeStartedTouching = Time.time;
                _startTouchPosition = _myTouch.position;
                break;
            case TouchPhase.Ended:
                _finalTouchPosition = _myTouch.position;
				
                // checking the delta
                var direction = _finalTouchPosition - _startTouchPosition;
                if (direction.magnitude > 60)
                {
                    _shouldCallFunc = true;
                }
                if (direction.x > 0)
                {
                    _swipeDirection = Vector2.right;
                }
                else if (direction.x < 0)
                {
                    _swipeDirection = Vector2.left;
                }

                break;
            case TouchPhase.Moved:
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Canceled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
		
        if (!_shouldCallFunc) return;
        if (_swipeDirection == Vector2.right)
        {
            SpecialsController.RotateSpecial += 1;    
        }
        else if (_swipeDirection == Vector2.left)
        {
            SpecialsController.RotateSpecial -= 1;
        }
        
        _shouldCallFunc = false;
    }
}

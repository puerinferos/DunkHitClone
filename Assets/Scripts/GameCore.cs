using System;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Trajectory trajectory;
    [SerializeField] private BasketBallHoopManager basketBallHoopManager;
    [SerializeField] private MainCamera mainCamera;
    [SerializeField] private UICore uiCore;

    [SerializeField] private CoreContext _context;

    private Input _input;

    public Action OnLoose;

    public Input Input => _input;

    private int _perfectCount = 0;
    private int _hitCount = 0;

    private void Start()
    {
        _input = new Input(_context.maxInputRange);

        _input.OnMouseUp += OnMouseUpHandler;
        _input.OnMouse += OnMouseHandler;
        _input.OnMouseDown += OnMouseDownHandler;
        
        mainCamera.Initialize(ball.transform);
        
        ball.Initialize(_context.ballStartPosition);

        basketBallHoopManager.Initialize(_context, this);
        basketBallHoopManager.OnCurrentHoopHit += OnHoopHitHandler;

        uiCore.GetWindow<MenuScreen>().OnOpen += ResetAllPositions;
        OnLoose += uiCore.GetWindow<LooseScreen>().Open;

        ResetAllPositions();
    }

    public void ResetPerfect() =>
        _perfectCount = -1;

    public void IncrementPerfect() =>
        ++_perfectCount;

    private void ResetAllPositions()
    {
        _input.UnBlockInput();
        ball.SetStartPosition();
        basketBallHoopManager.SetStartPosition();
        mainCamera.ResetPosition();

        _hitCount = 0;
        ResetPerfect();
        uiCore.GetWindow<InGameScreen>().UpdateCounter(_hitCount);
    }

    private void OnLooseHandler()
    {
        if (_hitCount > PlayerInfo.MaxCounter)
            PlayerInfo.MaxCounter = _hitCount;
    }

    private void OnHoopHitHandler(BasketBallHoop hoop,bool isCurrent)
    {
        ball.FreezeVelocity(hoop.transform);
        _input.UnBlockInput();
        if (isCurrent) 
            return;
        _hitCount += _perfectCount + 1;

        uiCore.GetWindow<InGameScreen>().UpdateCounter(_hitCount);
    }

    private void OnMouseHandler(Vector2 distance) =>
        trajectory.ShowTrajectory(ball.transform.position, distance * .02f);

    private void OnMouseUpHandler()
    {
        ball.SetVelocity(_input.Distance * .02f);
        trajectory.HideTrajectory();
    }

    private void OnMouseDownHandler()
    {
        uiCore.CloseWindow<MenuScreen>();
        _input.OnMouseDown -= uiCore.CloseWindow<MenuScreen>;
    }

    private void Update()
    {
        _input.DetectInput();
    }
}
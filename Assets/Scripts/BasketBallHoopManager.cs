using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasketBallHoopManager : MonoBehaviour
{
    [SerializeField] private BasketBallHoop hoopPrefab;
    [SerializeField] private CollectibleStar starPrefab;

    private CoreContext _coreContext;
    private GameCore _core;

    private Queue<BasketBallHoop> _hoops = new Queue<BasketBallHoop>();

    private BasketBallHoop currentNet;
    private BasketBallHoop nextNet;

    private CollectibleStar currentStar;
    
    private Vector2 _widthViewPortMinMax;

    public Action<BasketBallHoop,bool> OnCurrentHoopHit;

    public void SetStartPosition()
    {
        if(currentNet != null)
            RemoveNet(currentNet);
        if(nextNet != null)
            RemoveNet(nextNet);
        
        currentNet = AddNet(_coreContext.hoopStartPosition);
        currentNet.IsCurrent = true;
        nextNet = AddNet(NextHoopPosition());
    }

    public void Initialize(CoreContext coreContext, GameCore core)
    {
        _coreContext = coreContext;
        _core = core;

        if (Camera.main != null)
            _widthViewPortMinMax = new Vector2(Camera.main.ViewportToWorldPoint(Vector3.zero).x,
                Camera.main.ViewportToWorldPoint(Vector3.one).x);

        for (int i = 0; i < coreContext.hoopStartCount; i++)
        {
            var clone = Instantiate(hoopPrefab);
            clone.Initialize(core, core.Input);
            clone.gameObject.SetActive(false);
            clone.OnHit += OnHitHandler;

            _hoops.Enqueue(clone);
        }

        currentStar = Instantiate(starPrefab);
        currentStar.gameObject.SetActive(false);
    }

    private void OnHitHandler(bool isCurrent)
    {
        if(!isCurrent)
            AddNext();
        OnCurrentHoopHit?.Invoke(currentNet,isCurrent);
    }

    private void AddNext()
    {
        if (currentNet != null)
            RemoveNet(currentNet);
        currentNet = nextNet;
        currentNet.IsCurrent = true;
        currentNet.ResetTransform();

        var spawnPosition = NextHoopPosition();
        nextNet = AddNet(spawnPosition);
        SpawnStar(spawnPosition + Vector2.up * 1.5f);

        _core.IncrementPerfect();
    }

    private void SpawnStar(Vector2 position)
    {
        if(Random.Range(0f,1f) > _coreContext.starChance)
            return;
        
        currentStar.gameObject.SetActive(true);
        currentStar.transform.position = position;
    }

    private BasketBallHoop AddNet(Vector2 position)
    {
        var ballHoop = _hoops.Dequeue();
        ballHoop.gameObject.SetActive(true);
        ballHoop.transform.position = position;
        ballHoop.ResetTransform();

        return ballHoop;
    }

    private void RemoveNet(BasketBallHoop hoopToRemove)
    {
        hoopToRemove.IsCurrent = false;
        _hoops.Enqueue(hoopToRemove);
        hoopToRemove.gameObject.SetActive(false);
    }

    private Vector2 NextHoopPosition()
    {
        var position = currentNet.transform.position;

        float[] positions = new[]
        {
            _widthViewPortMinMax.x + .6f,
            position.x - 1.5f,
            position.x + 1.5f,
            _widthViewPortMinMax.y - .6f
        };

        bool isEnoughSpaceLeft = Mathf.Abs(positions[0] - position.x) > 1;

        float leftRandom = Random.Range(positions[0], positions[1]);
        float rightRandom = Random.Range(positions[2], positions[3]);

        float yRandom = position.y + Random.Range(_coreContext.yOffsetMinMax.x, _coreContext.yOffsetMinMax.y);

        float xRandom = isEnoughSpaceLeft ? leftRandom : rightRandom;

        return new Vector2(xRandom, yRandom);
    }
}
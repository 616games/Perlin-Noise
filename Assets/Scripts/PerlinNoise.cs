using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoise : MonoBehaviour
{
    #region --Fields / Properties--
    
    /// <summary>
    /// Controls the random X offset of the Perlin noise graph.
    /// </summary>
    private float _xOffset;
    
    /// <summary>
    /// Controls the random Y offset of the Perlin noise graph.
    /// </summary>
    private float _yOffset;

    /// <summary>
    /// Recalculation time for new random offsets of the Perlin noise graph.
    /// </summary>
    private float _time = .2f;
    
    /// <summary>
    /// Recalculation timer to track current _time.
    /// </summary>
    private float _timer;
    
    /// <summary>
    /// Position in world space of game object to apply Perlin noise.
    /// </summary>
    private Vector3 _position;

    /// <summary>
    /// Cached Transform component.
    /// </summary>
    private Transform _transform;
    
    #endregion

    #region --Unity Specific Methods--

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        //Track recalculation _timer and _time.
        _timer += Time.deltaTime;
        if (_timer > _time)
        {
            CalculatePerlinNoise();
        }
        
        //Update position with random Perlin noise offsets.
        _transform.position += _position * Time.deltaTime;
        
        //Increment offsets to continue down the Perlin noise graph.
        _xOffset += .1f;
        _yOffset += .1f;
    }
    
    #endregion
    
    #region --Custom Methods--

    /// <summary>
    /// Calculate random values to be used from the Perlin noise graph and apply it to the game object's position.
    /// </summary>
    private void CalculatePerlinNoise()
    {
        float _random = Random.Range(-50.0f, 50.0f);
        float _perlinNoise = Mathf.PerlinNoise(_xOffset, _yOffset);
        
        _position = transform.position;
        _position.x = _perlinNoise * _random;

        _random = Random.Range(-50.0f, 50.0f);
        _position.y = _perlinNoise * _random;

        _position.z = Random.Range(_position.x, _position.y);

        _time = Random.Range(.01f, .1f);
        _timer = 0;
    }
    
    #endregion
}

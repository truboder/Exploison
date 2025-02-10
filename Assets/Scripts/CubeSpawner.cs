using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefub;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private Transform _transform;
    [SerializeField] private Renderer _renderer;

    private float _splitChance = 1.0f;

    private void OnEnable()
    {
        _explosion.OnExplosion += Split;        
    }

    private void OnDisable()
    {
        _explosion.OnExplosion -= Split;
    }

    private void Split()
    {       
        if (CanSplit() == true)
        {
            float scaleReduction = 2;

            int minRange = 2;
            int maxRange = 6;

            int count = Random.Range(minRange, maxRange + 1);

            Color color = new (Random.value, Random.value, Random.value);
            _renderer.material.color = color;
            _cubePrefub.transform.localScale /= scaleReduction;

            GameObject cube = Instantiate(_cubePrefub, _transform.position, Quaternion.identity);          

            for (int i = 0; i < count; i++)
            {
                cube.GetComponent<CubeSpawner>()._splitChance /= 2.0f;
                Instantiate(cube);
            }          
        }
        else
        {
            Debug.Log("Не удалось разделиться");
        }
    }

    private bool CanSplit()
    {       
        if (Random.value <= _splitChance)
        {
            return true;
        }

        return false;
    }
}

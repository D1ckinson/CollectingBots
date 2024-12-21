using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCreator : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Flag _flag;
    [SerializeField] private Transform _emptyObject;
    [SerializeField] private Vector3 _basePoint = new(8.5f, 2.5f, 54.5f);
    [SerializeField] private int _botsCount = 3;
    [SerializeField] private int _baseCost = 5;

    private List<Base> _bases = new();
    private Plane _ground = new(Vector3.up, Vector3.zero);
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        InitBase(_botsCount);
        InitEmptyObject();
    }

    private void InitBase(int botsCount)
    {
        Base @base = Instantiate(_basePrefab, _basePoint, Quaternion.identity);
        _bases.Add(@base);

        @base.WasClicked += Build;

        for (int i = 0; i < botsCount; i++)
            @base.InitBot();
    }

    private void Build(Base @base) =>
        StartCoroutine(MoveEmptyObject(@base));

    private IEnumerator MoveEmptyObject(Base @base)
    {
        _emptyObject.gameObject.SetActive(true);
        bool isRun = true;
        Ray ray;
        Vector3 worldPosition = default;

        while (isRun)
        {
            yield return null;

            ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_ground.Raycast(ray, out float position) == false)
                continue;

            worldPosition = ray.GetPoint(position);
            _emptyObject.transform.position = worldPosition;

            if (Input.GetMouseButtonDown(0))
                isRun = false;
        }

        _emptyObject.gameObject.SetActive(false);
        Instantiate(_flag, worldPosition, Quaternion.identity);
        //@base.AmassResources(_baseCost);
    }

    private void InitEmptyObject()
    {
        MeshRenderer meshRenderer = _emptyObject.gameObject.AddComponent<MeshRenderer>();
        MeshRenderer thisMeshRenderer = _basePrefab.GetComponent<MeshRenderer>();
        meshRenderer.material = thisMeshRenderer.sharedMaterial;

        MeshFilter meshFilter = _emptyObject.AddComponent<MeshFilter>();
        MeshFilter thisMeshFilter = _basePrefab.GetComponent<MeshFilter>();
        meshFilter.mesh = thisMeshFilter.sharedMesh;

        _emptyObject.gameObject.SetActive(false);

    }

    private void OnDisable() =>
        _bases.ForEach(@base => @base.WasClicked -= Build);
}

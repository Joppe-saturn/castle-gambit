using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreviewPiece : MonoBehaviour
{
    private DataManager _dataManager;

    private MeshFilter _meshFilter;

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        _meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        _meshFilter.mesh = null;

        if (_dataManager.CurrentTower != null) 
        {
            Ray _ray;
            RaycastHit _hit;

            Vector3 mousePos = new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, 0);
            
            _ray = Camera.main.ScreenPointToRay(mousePos);
            
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_hit.transform.GetComponent<TowerTile>() != null)
                {
                    _meshFilter.mesh = _dataManager.CurrentTower.PrelookMesh;
                    transform.position = _hit.transform.position + new Vector3(-0.5f, 0, 0.5f);
                }
            }
        } 
    }
}

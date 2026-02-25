using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    private bool _isOpen = true;

    public bool IsOpen => _isOpen;

    public void OpenPoint(bool isOpen) => _isOpen = isOpen;
}
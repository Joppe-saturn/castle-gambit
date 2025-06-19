using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

public class ChessPiecesBase : MonoBehaviour
{
    protected bool closeToRook;
    private int _howManyRooks;

    public virtual void CheckForRook(DataManager pInstance, Vector3 pPos) // Rooks geeft een powerup aan de chess pieces in de buurt, deze functie checkt of er een rook in de buurt is.
    {
        if (_howManyRooks != pInstance.Rooks.Count) // Als de list niet geupdate is gaat, word er niet nog een keer gecheckt.
        {
            _howManyRooks = pInstance.Rooks.Count;
            closeToRook = false;
            for (int i = 0; i < _howManyRooks; i++)
        {
                Debug.Log(Mathf.Abs((pPos - pInstance.Rooks[i].transform.GetChild(0).position).magnitude));
                if (Mathf.Abs((pPos - pInstance.Rooks[i].transform.GetChild(0).position).magnitude) < 1.5f)
                {
                    closeToRook = true; // Wordt in de child class gebruikt.
                    break;
                }
            }
        }
    }
}

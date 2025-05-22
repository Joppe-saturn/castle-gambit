using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class ChessPiecesBase : MonoBehaviour
{
    [HideInInspector] public bool closeToRook;
    private int _howManyRook;

    public virtual void CheckForRook(DataManager pInstance) // Rooks geeft een powerup aan de chess pieces in de buurt, deze functie checkt of er een rook in de buurt is.
    {
        if (_howManyRook == pInstance.Rooks.Count) // Als de list niet geupdate is gaat, word er niet nog een keer gecheckt.
        {
            _howManyRook = pInstance.Rooks.Count;
            for (int i = 0; i < _howManyRook; i++)
            {
                if (Mathf.Abs((transform.position - pInstance.Rooks[i].transform.position).magnitude) < 1)
                {
                    closeToRook = true; // Wordt in de child class gebruikt.
                }
                else
                {
                    closeToRook = false;
                }
            }
        }
    }
}

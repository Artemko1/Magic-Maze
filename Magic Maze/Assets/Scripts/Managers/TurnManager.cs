using UnityEngine;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {
        public TurnPhase TurnPhase;
    }

    public enum TurnPhase
    {
        ColumnMove,
        PlayerMove
    }
}

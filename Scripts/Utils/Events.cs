using UnityEngine.Events;


public class Events
{
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    [System.Serializable] public class EventGameState : UnityEvent<GameManagerTest.GameState, GameManagerTest.GameState> { }

}

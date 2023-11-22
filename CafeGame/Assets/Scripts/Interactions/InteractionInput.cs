using UnityEngine;

public class InteractionInput : MonoBehaviour
{
    [SerializeField] 
    private bool can_interact = true;
    
    [SerializeField]
    private bool single_use;
    
    [SerializeField] 
    InteractionType interaction_type;
    
    [Header("Only used if InteractionType is set to 'Toggle'")]
    [Tooltip("Initial state of toggle. Only used when InteractionType is set to 'Toggle'.")]
    [SerializeField] 
    ToggleState toggle_state = ToggleState.Off;
    
    [Header("Events\nNOTE: Use 'Dynamic' methods from top of the list.")]
    [SerializeField]
    InteractionHeldItemEvent interaction_event;
    
    [Header("Off Event only used with InteractionType 'Toggle'")]
    [SerializeField] 
    InteractionHeldItemEvent interactionOffEvent;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public bool Interact(HeldItem item)
    {
        if (can_interact)
        {
            if (interaction_type == InteractionType.Trigger)
            {
                interaction_event?.Invoke(item);
                if (single_use)
                {
                    can_interact = false;
                }
            }
            else
            {
                switch (toggle_state)
                {
                    case ToggleState.On:
                        toggle_state = ToggleState.Off;
                        interactionOffEvent?.Invoke(item);
                        break;
                    case ToggleState.Off:
                        toggle_state = ToggleState.On;
                        interaction_event?.Invoke(item);
                        break;
                }

                if (single_use)
                {
                    can_interact = false;
                }
            }
            
            return true;
        }

        return false;
    }
}

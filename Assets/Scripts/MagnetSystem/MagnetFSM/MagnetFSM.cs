using Core.FSM;
using System.Collections.Generic;

namespace MagnetSystem.MagnetFSM
{
    public class MagnetFSMTransitionMessage : FSMTransitionMessage { }
    
    public class MagnetFSM : FSMBase<EMagnetState, MagnetFSMTransitionMessage>
    {
        private readonly IReadOnlyDictionary<EMagnetState, HashSet<EMagnetState>> _transitionMappings =
            new Dictionary<EMagnetState, HashSet<EMagnetState>>()
            {
                { EMagnetState.Idle, new HashSet<EMagnetState>() { EMagnetState.Active } },
                { EMagnetState.Active, new HashSet<EMagnetState>() { EMagnetState.Idle, EMagnetState.Holding } },
                { EMagnetState.Holding, new HashSet<EMagnetState>() { EMagnetState.Idle } },
            };

        protected override IReadOnlyDictionary<EMagnetState, HashSet<EMagnetState>> GetTransitionMappings()
        {
            return _transitionMappings;
        }
    }
}
using Core.FSM;
using System.Collections.Generic;

namespace CharacterSystem
{
    public enum ECharacterState
    {
        None = 0,
        Idle = 10,
        Move = 20,
    }
    
    public class CharacterFSM : FSMBase<ECharacterState, CharacterFSMTransitionMessage>
    {
        protected override IReadOnlyDictionary<ECharacterState, HashSet<ECharacterState>> GetTransitionMappings()
        {
            return new Dictionary<ECharacterState, HashSet<ECharacterState>>()
            {
                { ECharacterState.Idle, new HashSet<ECharacterState>() { ECharacterState.Move } },
                { ECharacterState.Move, new HashSet<ECharacterState>() { ECharacterState.Idle } },
            };
        }
    }
}
using System;
using BugStrategy.Libs;
using BugStrategy.Unit;
using BugStrategy.Unit.ProcessorsCore;
using CycleFramework.Extensions;

namespace BugStrategy.Ai.InternalAis
{
    public class AttackOrderEvaluator : UnitInternalEvaluator
    {
        private readonly IReadOnlyAttackProcessor _attackProcessor;
        private ITarget _hashedTarget;

        public AttackOrderEvaluator(UnitBase unit, InternalAiBase internalAi, IReadOnlyAttackProcessor attackProcessor)
            : base(unit, internalAi)
        {
            _attackProcessor = attackProcessor;
        }

        public override float Evaluate()
        {
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.Auto:
                    if (!Unit.CurrentPathData.Target.IsAnyNull() &&
                        Unit.CurrentPathData.Target.Affiliation != Unit.Affiliation &&
                        Unit.CurrentPathData.Target.CastPossible<IDamagable>())
                    {
                        _hashedTarget = Unit.CurrentPathData.Target;
                        return 5;
                    }

                    if (_attackProcessor.CheckEnemiesInAttackZone())
                    {
                        _hashedTarget = null;
                        return 1;
                    }

                    if (!UnitInternalAi.Attacker.IsAnyNull())
                    {
                        _hashedTarget = UnitInternalAi.Attacker;
                        return 1;
                    }
                    
                    break;
                case AiUnitStateType.Attack:
                    if (_attackProcessor.TargetInZone(UnitInternalAi.GlobalAiOrderTarget))
                    {
                        _hashedTarget = UnitInternalAi.GlobalAiOrderTarget;
                        return 10;
                    }

                    if (!Unit.CurrentPathData.Target.IsAnyNull() &&
                        Unit.CurrentPathData.Target.Affiliation != Unit.Affiliation &&
                        Unit.CurrentPathData.Target.CastPossible<IDamagable>() &&
                        _attackProcessor.TargetInZone(Unit.CurrentPathData.Target))
                    {
                        _hashedTarget = Unit.CurrentPathData.Target;
                        return 5;
                    }

                    if (_attackProcessor.CheckEnemiesInAttackZone())
                    {
                        _hashedTarget = null;
                        return 2;
                    }
                    
                    if (!UnitInternalAi.GlobalAiOrderTarget.IsAnyNull())
                    {
                        _hashedTarget = UnitInternalAi.GlobalAiOrderTarget;
                        return 1;
                    }

                    if (!UnitInternalAi.Attacker.IsAnyNull())
                    {
                        _hashedTarget = UnitInternalAi.Attacker;
                        return 1;
                    }
                    
                    break;
                case AiUnitStateType.Idle:
                    if (_attackProcessor.CheckEnemiesInAttackZone())
                    {
                        _hashedTarget = null;
                        return 2;
                    }
                    
                    if (!UnitInternalAi.Attacker.IsAnyNull())
                    {
                        _hashedTarget = UnitInternalAi.Attacker;
                        return 1;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _hashedTarget = null;
            return float.MinValue;
        }

        public override void Apply()
        {
            Unit.HandleGiveOrder(_hashedTarget, UnitPathType.Attack);
        }
    }
}
using System.Collections.Generic;
using WarToolkit.ObjectData;

namespace WarToolkit.Core
{
    /// <summary>
    /// Interface for calculating battle outcomes between attacking and defending units.
    /// </summary>
    public interface ICombatEvaluator
    {
        /// <summary>
        /// Evaluate the combat outcome between multiple attackers and a single defender.
        /// </summary>
        /// <param name="attackers">List of attacking units</param>
        /// <param name="defender">Defending unit</param>
        /// <returns>Combat outcome as a point score.</returns>
        /// <remarks>
        /// [+2: defender destroyed]
        /// [+1: defender damaged]
        /// [0: attacker damaged, defender damaged]
        /// [-1: attacker damaged]
        /// [-2: attacker destroyed]
        /// </remarks>
        int EvaluateCombat(List<IUnit> attackers, IUnit defender);
    }
}
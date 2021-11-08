using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constants
{
    public static class FormationConstants
    {
        public static int FormationMatrixSize = 5;
        public static int MaxPlayersInFormationCount = 11;
        public static int MaxPlayersOnBenchCount = 7;

        /// <summary>
        /// Dictionary stores all player positions and correct locations in formation.
        /// This could be set as const in separate file (but its good for quick implementation).
        /// </summary>
        public static Dictionary<PlayerPosition, List<(int, int)>> PlayerPositionsLocations = new()
        {
            { PlayerPosition.GK, new() { (4, 2) } },
            { PlayerPosition.CB, new() { (3, 1), (3, 2), (3, 3) } },
            { PlayerPosition.LB, new() { (3, 0) } },
            { PlayerPosition.RB, new() { (3, 4) } },
            { PlayerPosition.LWB, new() { (2, 0) } },
            { PlayerPosition.RWB, new() { (2, 4) } },
            { PlayerPosition.CDM, new() { (2, 1), (2, 2), (2, 3) } },
            { PlayerPosition.CM, new() { (2, 1), (2, 2), (2, 3) } },
            { PlayerPosition.CAM, new() { (1, 1), (1, 2), (1, 3) } },
            { PlayerPosition.LM, new() { (1, 0), (2, 0) } },
            { PlayerPosition.LW, new() { (0, 0), (1, 0) } },
            { PlayerPosition.RM, new() { (1, 4), (2, 4) } },
            { PlayerPosition.RW, new() { (0, 4), (1, 4) } },
            { PlayerPosition.CF, new() { (0, 1), (0, 2), (0, 3), (1, 1), (1, 2), (1, 3) } },
            { PlayerPosition.ST, new() { (0, 1), (0, 2), (0, 3) } },
        };
    }
}

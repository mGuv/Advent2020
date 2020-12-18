
using System;
using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day17
{
    /// <summary>
    /// Solution to Day 17 - Part A - 3D Space Conway
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            HashSet<(int x, int y, int z)> map = new HashSet<(int x, int y, int z)>();


            string[] input = this.GetInput(arguments);
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        map.Add((x, y, 0));
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                // Check map, marking all positions around it with a 1
                Dictionary<(int x, int y, int z), int> activeNeighbourCount = new Dictionary<(int x, int y, int z), int>();
                HashSet<(int x, int y, int z)> newMap = new HashSet<(int x, int y, int z)>();
                foreach ((int x, int y, int z) activeCell in map)
                {
                    int neighboursFound = 0;
                    for (int x = activeCell.x - 1; x <= activeCell.x + 1; x++)
                    {
                        for (int y = activeCell.y - 1; y <= activeCell.y + 1; y++)
                        {
                            for (int z = activeCell.z - 1; z <= activeCell.z + 1; z++)
                            {
                                // skip 0,0,0
                                if (x == activeCell.x && y == activeCell.y && z == activeCell.z)
                                {
                                    continue;
                                }

                                if (map.Contains((x, y, z)))
                                {
                                    neighboursFound++;
                                    continue;
                                }

                                if (!activeNeighbourCount.ContainsKey((x, y, z)))
                                {
                                    activeNeighbourCount[(x, y, z)] = 0;
                                }

                                activeNeighbourCount[(x, y, z)] = activeNeighbourCount[(x, y, z)] + 1;
                            }
                        }
                    }

                    if (neighboursFound == 2 || neighboursFound == 3)
                    {
                        newMap.Add(activeCell);
                    }
                }

                foreach (KeyValuePair<(int x, int y, int z),int> positionCount in activeNeighbourCount)
                {
                    if (positionCount.Value != 3)
                    {
                        continue;
                    }

                    newMap.Add(positionCount.Key);
                }

                map = newMap;
            }

            return $"{map.Count}";
        }
    }
}

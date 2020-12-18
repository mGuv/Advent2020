
using System;
using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day17
{
    /// <summary>
    /// Solution to Day 17 - Part B - 4D Space Conway
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            HashSet<(int x, int y, int z, int w)> map = new HashSet<(int x, int y, int z, int w)>();


            string[] input = this.GetInput(arguments);
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        map.Add((x, y, 0, 0));
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                // Check map, marking all positions around it with a 1
                Dictionary<(int x, int y, int z, int w), int> activeNeighbourCount = new Dictionary<(int x, int y, int z, int w), int>();
                HashSet<(int x, int y, int z, int w)> newMap = new HashSet<(int x, int y, int z, int w)>();
                foreach ((int x, int y, int z, int w) activeCell in map)
                {
                    int neighboursFound = 0;
                    for (int x = activeCell.x - 1; x <= activeCell.x + 1; x++)
                    {
                        for (int y = activeCell.y - 1; y <= activeCell.y + 1; y++)
                        {
                            for (int z = activeCell.z - 1; z <= activeCell.z + 1; z++)
                            {
                                for (int w = activeCell.w - 1; w <= activeCell.w + 1; w++)
                                {
                                    // skip 0,0,0,0
                                    if (x == activeCell.x && y == activeCell.y && z == activeCell.z && w == activeCell.w)
                                    {
                                        continue;
                                    }

                                    if (map.Contains((x, y, z, w)))
                                    {
                                        neighboursFound++;
                                        continue;
                                    }

                                    if (!activeNeighbourCount.ContainsKey((x, y, z, w)))
                                    {
                                        activeNeighbourCount[(x, y, z, w)] = 0;
                                    }

                                    activeNeighbourCount[(x, y, z, w)] = activeNeighbourCount[(x, y, z, w)] + 1;
                                }
                            }
                        }
                    }

                    if (neighboursFound == 2 || neighboursFound == 3)
                    {
                        newMap.Add(activeCell);
                    }
                }

                foreach (KeyValuePair<(int x, int y, int z, int w),int> positionCount in activeNeighbourCount)
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day11
{
    /// <summary>
    /// Solution to Day 11 - Part A - Conway's game of life (kind of)
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);

            // Create initial floor plan of (x, y) => seat state
            Dictionary<(int x, int y), State> floorPlan = new Dictionary<(int x, int y), State>();
            for (int y = 0; y < input.Length; y++)
            {
                for(int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == 'L')
                    {
                        // Every seat is taken in the first found due to all seats being unseated so we can skip one step here
                        floorPlan.Add((x, y), State.Seated);
                    }
                    else
                    {
                        floorPlan.Add((x, y), State.Floor);
                    }
                }
            }

            // All the possible neighbouring seats that we care about
            (int x, int y)[] neighbourOffsets =
            {
                (-1, -1),
                (0, -1),
                (1, -1),
                (-1, 0),
                (1, 0),
                (-1, 1),
                (0, 1),
                (1, 1)
            };

            // Keep looping until nothing changes
            while (true)
            {
                // Build a new floor plan as we cannot modify the old floor plan as conway's is done in stages, not cumulative
                Dictionary<(int x, int y), State> newFloorPlan = new Dictionary<(int x, int y), State>();

                // Lazy way to detect changes
                int changes = 0;

                // Loop through floor plan dimensions, lazy to capture these as vars so just use input size again
                for (int y = 0; y < input.Length; y++)
                {
                    for(int x = 0; x < input[y].Length; x++)
                    {
                        // If it's floor, it's always floor
                        if (floorPlan[(x, y)] == State.Floor)
                        {
                            newFloorPlan.Add((x,y), State.Floor);
                            continue;
                        }

                        // Count the amount of neighbouring seats
                        int seatedNeighbours = 0;
                        foreach ((int x, int y) neighbourOffset in neighbourOffsets)
                        {
                            // Use neighbour offsets to scan around seat
                            int neighbourX = neighbourOffset.x + x;
                            int neighbourY = neighbourOffset.y + y;

                            // Ensure seat is actually in the floor plan as this can fall outside of floor plan
                            if (floorPlan.TryGetValue((neighbourX, neighbourY), out var state))
                            {
                                // Seat in floor plan, count how many taken
                                if (state == State.Seated)
                                {
                                    seatedNeighbours++;
                                }
                            }
                        }

                        // If this seat was taken, check if we need to unseat due to too many seated neighbours
                        if (floorPlan[(x, y)] == State.Seated)
                        {
                            if (seatedNeighbours >= 4)
                            {
                                // Flip the seat and record it
                                newFloorPlan.Add((x, y), State.Unseated);
                                changes++;
                            }
                            else
                            {
                                // Not enough neighbours, stay seated
                                newFloorPlan.Add((x, y), State.Seated);
                            }
                        }
                        // If this seat wasn't taken, check if someone can sit here due to no seated neighbours
                        else if (floorPlan[(x, y)] == State.Unseated)
                        {
                            // check for 0
                            if (seatedNeighbours == 0)
                            {
                                // Flip the seat and record it
                                changes++;
                                newFloorPlan.Add((x, y), State.Seated);
                            }
                            else
                            {
                                // Too many neighbours, don't sit here
                                newFloorPlan.Add((x, y), State.Unseated);
                            }
                        }
                    }
                }

                // Update floor plan
                floorPlan = newFloorPlan;

                // Escape loop if nothing changed
                if (changes == 0)
                {
                    break;
                }
            }

            // Count seated seats
            int occupied = floorPlan.Values.Count(s => s == State.Seated);
            return $"occupied: {occupied}";
        }
    }
}

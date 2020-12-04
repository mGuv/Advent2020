# Advent 2020

This is my attempt at solving all the problems presented by Advent of Code 2020.

## Installation

- Install .NET 5 SDK, as I haven't wrapped this in a docker container yet
- Checkout this repository

## Usage

This year, the application needs to run entirely with command arguments. So navigate to the `./Runner/` folder and use `dotnet run`.

### p

`p` specifies the problem to run and should be written in the from of `DayX.ProblemY`. An example of this would be:

```
dotnet run -- p=Day1.ProblemA
```

### f

`f` is required by any problems that utilise file input and should be the relative path to the input file. An example of this would be:

```
dotnet run -- p=Day1.ProblemA f=Problems\Day1\AInput.txt
```

### i
`i` is required by any problems that utilise command line input. An example of this would be:

```
dotnet run -- p=Day1.ProblemA i="This is the problem Input"
```

## Extending

Each Day must be added as a sub-directory in the `Problems` directory, the name of the new folder must be `DayX`, e.g. on Day 2 the folder `Problems\Day2` must be created.

Inside of that folder, should be the sub-problems of that day, as `ProblemA.cs` and `ProblemB.cs`, the classes must be named similar.

Any input files should also be placed here so they are associated to their given Day.

Finally, any extra code, classes, etc. needed for a given Day should exist inside that Days folder. Should it come to any scenario where there are cross day problems, any shared code should be elevated to a directory in the root of the `Runner` project.
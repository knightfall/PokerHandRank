# Poker Hand Sorter

Simple poker evaluator.

## Requirements

This program has the following requirements

- **Language:** .NET Core SDK 3.1
- **OS:**
  - Windows 10
  - macOS (10.13 - 10.15)
  - Ubuntu LTS / CentOS 8 / For complete list visit [.NET Installation for Linux](https://docs.microsoft.com/en-us/dotnet/core/install/linux)

## Installation

As this repo doesn't come with a compiled version, you will have to install .NET SDK on your computer.

### Windows & macOS

1. Visit [.NET Core download page](https://dotnet.microsoft.com/download) to download .NET Core SDK 3.1
2. Click on ***macOS*** tab if you want to install on macOS. Otherwise skip to step 3.
3. Click on ***Download .NET Core SDK*** to download the installation file.
4. Install the SDK.

### Linux

- Follow the [Install .NET Core on Linux documentation](https://docs.microsoft.com/en-us/dotnet/core/install/linux)

## Running the program

In the directory of the program, execute the command below. the command below assumes
the test file is in the program directory..

### Windows CMD

```cmd
C:\PokerHandSorter>type poker-hands.txt | dotnet run
```

### Powershell

```powershell
C:\PokerHandSorter>cat poker-hands.txt | dotnet run
```

### Bash

```bash
[poker@tester ~]$ cat poker-hands.txt | dotnet run
```

Successful execution will output the following

```bash
Player 1: 263
Player 2: 237
```

# Sudoku Generator And Solver With Export Functionality
![Programming Lang](https://img.shields.io/badge/Language-C%23-brightgreen)
![Type](https://img.shields.io/badge/Type-Console-8d32a8)
![Framework](https://img.shields.io/badge/Framework-.Net%206.0-%23034efc)
![Platform](https://img.shields.io/badge/Platform-Windows-informational)

## Description
It Started As An Exercise In Java. Afterwards I Wanted To Recreate It In C# But More Ideas Kept Coming Up And This Is The Result.
My Teacher Told Me About The Next Topic Being Multi-Dimensional-Arrays And That We Take A Look At Sudoku.
So I Was Thinking About A Generator Which Was Relativly Easy Except For The Backtracking Part Of The Algorithm Because That Was Something New For Me.
After I Wrote The Program I Wanted To Recreate It In C# Which Lead To This Project.

## Idea
After Recreating It I Wanted To Make A Usable App Out Of It:

1. Generate A Valid Sudoku
2. Allow For Input For The Sudoku To Solve
3. Check Whether The Input Is A Valid Board
4. Solve The Sudoku
5. Allow For Export To A TXT File

## How To Use
- You Start In A [Menu](#menu) Where You Can Press (G) To Generate A Sudoku Or (S) To Solve One.
- When You Decide For (S)olving One You Can Enter Each Row With Or Without WhiteSpaces Depending On What You Prefer.
- If You Enter An Empty Line It Will Reset To Row [1]
- If You Have Anything Other Than Numbers In Your Input The Row Will Reset To [current] So You Don't Have To Start From The Beginning.

## Note
I Do Not Check For Permissions So Make Sure The App Can Create The File If It Does Not Exist Or Open And Append To An Existing One.

## Images

### Menu
<img width="476" alt="Start" src="https://user-images.githubusercontent.com/65088572/190224339-688338c4-88c0-4d10-89f2-c139b98fea71.png">

### Generate
<img width="771" alt="Generate" src="https://user-images.githubusercontent.com/65088572/190224370-ba61d798-8308-4cd6-abf8-8a2f251eb298.png">

### Export
<img width="436" alt="Export" src="https://user-images.githubusercontent.com/65088572/190224391-19e3f5b3-dfcd-4860-a75c-878fee0af60c.png">

### Result
<img width="632" alt="Export_Result" src="https://user-images.githubusercontent.com/65088572/190224404-44eef4ee-7ece-49cb-820e-76830b3c80dd.png">

### Input
<img width="485" alt="Input" src="https://user-images.githubusercontent.com/65088572/190224431-67c54848-9780-4541-b0d4-a99dab12dd52.png">

### Example
![Input_Example](https://user-images.githubusercontent.com/65088572/190224450-e37d9be7-76c0-4b29-9011-647974eb29e6.png)

### Solved
<img width="371" alt="Solved" src="https://user-images.githubusercontent.com/65088572/190224463-68aff039-9c05-462d-a146-0f13f0debb20.png">

### Exit
<img width="268" alt="End" src="https://user-images.githubusercontent.com/65088572/190224479-a287c544-a8ad-4e2f-be67-2d25081a0472.png">

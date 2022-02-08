Author:     Jack Machara
Partner:    None
Date:       1/29/20
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment #3 - Refactoring the FormulaEvaluator
Copyright:  CS 3500 and Jack Machara - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators:
    I found when debugging my dependencyGraph class that stresstest15 wont pass unless you add the elements to both
    stacks automatically so changing the test or telling future students they need to do this to not miss test points
    will save them hours of debugging and points.
    I didn't get 100% coverage because some of the branches shouldn't be visited unless something randomly goes wrong because
    the constructor checks for most cases.

2. Assignment Specific Topics
Hours Estimated/Worked         Assignment                       Note
          5    /   6    - Assignment 1 - Formula Evaluator     nothing took much longer than expected
	      7   /    8    - Assignment 2 - Dependency Graph	   Got caught up on Branches for awhile.
          8  /     10    - Assignment 3 - Refactoring the FormulaEvaluator

3. Consulted Peers:

N/A

4. References:
   
   formula evaluator
    1. How to convert a string to a number (C# Programming Guide) - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number
    2. How can I convert String to Int? - https://stackoverflow.com/questions/1019793/how-can-i-convert-string-to-int
    3. C# | Trim() Method - https://www.geeksforgeeks.org/c-sharp-trim-method/
    4. C# Regex.Split: Removing empty results - https://stackoverflow.com/questions/4912365/c-sharp-regex-split-removing-empty-results
    5. Regex.Split Method - https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.split?view=netframework-4.8
    6. HashSet<T> Class - https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=netframework-4.8
    7. IEnumerable Interface - https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netframework-4.8
    8. Branch testing - https://www.tutorialspoint.com/software_testing_dictionary/branch_testing.htm
    9. Can't find project classes/methods in test project - https://stackoverflow.com/questions/40515306/cant-find-project-classes-methods-in-test-project
    10. Convert from scientific notation string to float in C# - https://stackoverflow.com/questions/64639/convert-from-scientific-notation-string-to-float-in-c-sharp

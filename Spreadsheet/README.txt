Author:     Jack Machara
Partner:    Brian Dong
Date:       2/25/20
Course:     CS 3500, University of Utah, School of Computing
Assignment: Assignment #6 - Spreadsheet Front-End Graphical User Interface
Copyright:  CS 3500 and Jack Machara and Brian Dong - This work may not be copied for use in Academic Coursework.

1. Comments to Evaluators: More detailed information about old assignments can be found in their respective projects
Assignment 6: 
    GitRepo and Commit:https://github.com/uofu-cs3500-spring20/assignment-six-completed-spreadsheet-brian-jack-team.git b42010a719005c84ec1b02a0953765a94ecd9d67
    Partnership: All of the coding was done as pair programming; Jack largely implemented the background worker, while Brian
        did a lot of the smaller methods and troubleshooting.
    Branching: N/A
    Additional Features/Design: Seperate save and save as (view help menu for more specifics)
                                Spreadsheet movement with arrow keys, loops around ends
                                Ctrl+s for saving
                                Ctrl+Shift+s for saveAs
                                Ctril+n for new
                                Enter adds value and moves selection down by one
                                Tab adds the value and moves the selection to the right by one
                                Background worker used for adding to spreadsheet object, due to potentially taking a long time
                                if there are many dependencies involved.
                                Both element formatted and attributed formatted files work
                                Formulas form/evaluate after clicking off boxes after input
                                Selected box displays content rather than value in box
                                Help menu is done as popup messages
                                Circular dependencies display an error popup and then clear the offending cell
                                Formulas that cannot be evaluted, due to dependencies that are empty/not numbers display #Error

    Testing methods: Brian wrote several spreadsheet files that could be immediaetly loaded to test 1. loading functionality, 2. complex
    dependencies and responsiveness to modification to said dependencies 3. any other errors that popped up with more complex sheets. Then
    we also would simply load the spreadsheet and go through a checklist of things to evaluate, such as Circular dependency handling, formula
    creation, multi level dependencies, formulas referencing cells with strings, formulas referencing empty cells, saving and reloading a file
    to see that it creates the same spreadsheet, etc.

    Best Team Practices: 
                Good: We spent this entire assignment as a pair programming, which means that we had the ability to immediately 
                troubleshoot issues with each other. The only time spent apart was earlier in the week when Brian was sick.
                In addittion, we could divide up the work in a flexible manner, essentially getting two things done at once; 
                for example, Jack worked on implementing a background worker while Brian worked on reading functionality and help 
                menu text.

                Improve: Whiteboarding through problems, since occaisionally we would not understand what each other meant when
                describing an issue, kind of leading us to ignore it for the time being. In addition, we could also work more on
                dividing responsibilities and getting work done outside of pair programming, simply because pair time in the future
                may be more scarce. Sort of a software practice issue, on this assignment we could have done better on commenting
                as we worked, rather than all at the end, which would also help faciltate reading each other's code and integrating
                functionality.

    

2. Assignment Specific Topics
Hours Estimated/Worked         Assignment                                   Note
          5    /   6    - Assignment 1 - Formula Evaluator                              Nothing took much longer than expected
	      7    /   8    - Assignment 2 - Dependency Graph	                            Got caught up on Branches for awhile.
          8    /   10   - Assignment 3 - Refactoring the FormulaEvaluator               N/A
          10   /   9    - Assignment 4 - Onward to a Spreadsheet                        Draw out pictures first next Assignment
          10   /   23   - Assingment 5 - A "Complete" Spreadsheet Model                 I spent a lot of time on this because I improved A4 code but also had to 
                                                                                        change methods a lot because of misinterpretations. Time Debugging by my 
                                                                                        code wont run took many hours. 
          8   /   12   - Assingment 6 - Spreadsheet Front-End Graphical User Interface     

3. Consulted Peers: Brian Dong - Partner on assignment 6

4. References:
   
   formula evaluator
   Assignment 1
    1. How to convert a string to a number (C# Programming Guide) - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number
    2. How can I convert String to Int? - https://stackoverflow.com/questions/1019793/how-can-i-convert-string-to-int
    3. C# | Trim() Method - https://www.geeksforgeeks.org/c-sharp-trim-method/
    4. C# Regex.Split: Removing empty results - https://stackoverflow.com/questions/4912365/c-sharp-regex-split-removing-empty-results
    5. Regex.Split Method - https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.split?view=netframework-4.8
    6. HashSet<T> Class - https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=netframework-4.8
    7. IEnumerable Interface - https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netframework-4.8
    Assignment 2
    8. Branch testing - https://www.tutorialspoint.com/software_testing_dictionary/branch_testing.htm
    Assignment 3
    9. Can't find project classes/methods in test project - https://stackoverflow.com/questions/40515306/cant-find-project-classes-methods-in-test-project
    10. Convert from scientific notation string to float in C# - https://stackoverflow.com/questions/64639/convert-from-scientific-notation-string-to-float-in-c-sharp
    Assignment 4
    N/A
    Assignment 5
    11. How do you catch exceptions with “using” in C# - https://stackoverflow.com/questions/518292/how-do-you-catch-exceptions-with-using-in-c-sharp
    12. Reading Xml with XmlReader in C# - https://stackoverflow.com/questions/2441673/reading-xml-with-xmlreader-in-c-sharp
    13. PluralSight - Basics of Unit Testing for C# Developers Initialization and Cleanup
    Assignment 6
    14. How to get file path from OpenFileDialog and FolderBrowserDialog? - https://stackoverflow.com/questions/24449988/how-to-get-file-path-from-openfiledialog-and-folderbrowserdialog
    15. How to know user has clicked “X” or the “Close” button? - https://stackoverflow.com/questions/2683679/how-to-know-user-has-clicked-x-or-the-close-button
    16. Double confirmation on exit - https://stackoverflow.com/questions/4622051/double-confirmation-on-exit
    17. Override ProcessCmdKey C# - https://stackoverflow.com/questions/9882310/override-processcmdkey-c-sharp
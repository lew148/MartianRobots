# 🤖 Martian Robots
A challenge from Red Badger.
Shows end positions of robots given a grid, starting positions, orientation and instructions.

 # Tech Overview 
 > .Net 6.0 (and xUnit) Backend with React frontend

 * Focused on keeping .NET code readable and open to easy changes in the future
 * Methods have been abstracted into readable, editable and testable modules
 * Performance has been optimised with an estimated Big O of O(n), due to the linear requirements of the challenge
 * ```InputDataModel.GetInputDataFromArgs``` has been left in for testing and usability reasons
 * Frontend has been written with UX in mind

## How to run
* Open .sln in VS studio or Rider
* Restore nuget packages
* Run ```npm install``` in ClientApp folder
* Click run button
* Robots ✅

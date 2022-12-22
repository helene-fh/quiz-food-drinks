# Quiz-food-drinks-Backend  
## MADE BY HELENE AND VINCENT
## Description
Get a random question from our API, that gets a question from the Trivia API or from our database. If a question from Trivia isnt in our database it will be added.
We used Entity Framework Core 7, Sqlite, MVVM principle and with swagger for this project and it is coded in C#.

## Instructions
1. Clone the project down to your pc or mac
2. Install dotnet ef CLI [Link](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
3. Enter this in the CLI `dotnet ef migrations add InitialCreate` to get migrations, if they are missing
4. Enter this aswell in the CLI `dotnet ef database update` to make up a database named **QuizSqlLight.db** if it is missing
5. Launch Swagger in the browser by starting the project.

## Game Instructions
1. Press *"GET"* under the **"Quiz"** and press **"Try it out"** at the right side and **"Execute"** to get a Question with an id and relative answers
2. Make a guess what the right answer is
3. Copy the id and move up in swagger to **"Answer"** at the top
4. Press *"GET /api/Answer/{id}"* and press **"Try it out"** and paste the **"id"** in the id box and press **"Execute"**
5. Check if the answer is true, then you guessed it right, false and its the wrong answer 
6. Repeat the previous steps to keep the Quiz going



# GraphSolver Application

The GraphSolver application is a powerful tool for graph visualization, equation solving, and user management. This Windows Forms application, developed in C#, combines advanced mathematical computations with database management and REST services to provide a seamless and efficient experience for users.

## Features

- **Graph Display**: The GraphSolver application allows users to create and visualize graphs, with various customization options such as node colors, edge weights, and graph layout.

- **Equation Solver**: Users can input mathematical equations into the application, and the built-in equation solver can solve them accurately and quickly, providing solutions in real-time.

- **User Database**: The application utilizes MongoDB to securely store user commands and data. Users can create accounts, login, and manage their commands, which are stored in a MongoDB collection.

- **REST Services**: The GraphSolver application uses REST services to handle user authentication, command execution, and other functionalities, ensuring efficient communication between the application and the backend server.

- **Function Minimization**: The application also includes a function minimization feature, allowing users to optimize mathematical functions and find the minimum or maximum values using various optimization algorithms.

## Technologies Used

- C# (Windows Forms): The GraphSolver application is developed using C# programming language with Windows Forms for the graphical user interface.

- MongoDB: The application uses MongoDB, a popular NoSQL database, to store and manage user commands and data securely.

- REST Services: RESTful APIs are implemented to handle user authentication, command execution, and other functionalities, providing seamless communication between the application and the backend server.

## Getting Started

To run the GraphSolver application locally, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Restore the NuGet packages used in the application.
4. Update the MongoDB connection string in the application's configuration file to point to your MongoDB instance.
5. Build and run the application in Visual Studio.

Make sure to have MongoDB installed and configured on your machine to fully utilize the application's features.

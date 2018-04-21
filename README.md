# Tablut
This repository contains the source code of a game made in C#. The current game is the Tablut.

Here is the structure of the repository:

### Design:
- This folder contains the textures and the sounds used to make the game.

### Documentation:
- Holds the different documents made along the project.
- Those are the originals.

### Rendu:
- **This is the final folder of the project. All necessary files for end users are here.**
- It contains:
    - The project documentation
    - The code documentation
    - The game installer .msi
    - The work diary

### Tablut:
- This directory holds the C# solution that contains two projects:
    - Setup_Tablut: this one creates the installer
    - Tablut: it is the main project containing the game. Since I placed the library in the bin folder, Git ignored it. To launch the solution without error, just reference MySQL.data.dll in the "Tablut" folder. To do so, in Visual Studio, on the tool bar, click "Project" and then "Add reference".
    
### .SQL files:
- MHG_DB_Tablut.sql is the main sql files. He is provided when the game is installed using the .msi.
- MHG_DB_Tablut_Data.sql is a script used during tests. It creates fake users.

_The code is written in English but all the rest of documents are in French_

# Ghost game

In the game of Ghost, two players take turns building up an English word from left to right. Each player adds one letter per turn. The goal is to not complete the spelling of a word: if you add a letter that completes a word (of 4+ letters), or if you add a letter that produces a string that cannot be extended into a word, you lose. Basically, each player should try to extend the game as much as possible so the rival loses if one of those conditions are met.

This program  allows a user to play Ghost against the computer. It has been created as a **_ASP.NET Core 2.0 MVC_** project for the server side, using *_React_* for the frontend. All the necessary packages for the client side are obtained using **_Node.js_**. The Javascript and CSS minification has been performed using **_Webpack_**.

The computer should play optimally given the attached dictionary. By default, this dictionary is taken form the *wordlist.txt* present in the project folder. This behaviour can be changed in the *appsettings.json* file, using the parameter *DictionaryFileLocation*. Besides, minimum word length can not be less than 4 character, but this number can be modified using the parameter *MinimumWordLength*

For building and running the project it is necessary to have **_.NET Core 2.0_** and **_Node.js_ 5.6.0** or newer installed. 
This can be done via CLI calling from the project folder:

```
npm run build
dotnet build
```

And for running the project:

```
dotnet run
```

The game interface allows the user to interact with the server by clicking in the selected character and waiting its response. When the game is finished, the Restart game button can be used for restarting the game.

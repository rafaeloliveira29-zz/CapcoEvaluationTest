# CapcoEvaluationTest
Status: under Development.
____________________________________________________________________________________________________________________
Requirements:
- Use .NetCore - OK
- C# - OK
- WebApi - OK
- Restful - OK
- Http Protocol - OK
- JSON - OK
- GIT (Make the code available in an own repository) - OK 
___________________________________________________________________________________________________________________
Evaluation Test to Capco for interview process - Description

This is a .NetCore API that consumes the https://swapi.co/ (The star wars API) and shows some informations obtained from it.
There are 4 methods of type GET on the "on development" API.
  - Index();
  - GetCharacterAsync();
  - GetCharactersListByName();
  - GetHumanCharacter();
  
  The index page was designed to make easier the calling of the functions described above. The function GetCharactersAsync() returns
  the character id, the character name, the year of birth if availabe and the names of the movies this character has participated. The 
  function GetCharactersListByName() returns all the characters ordered by name and shows between the character name, the number of films 
  has participated. The function GetHumanCharacter() returns all human characters, their heights and calculate the mean between all the
  human character heights.
  ____________________________________________________________________________________________________________________________
  Tests:

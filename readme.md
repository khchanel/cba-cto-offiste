CBA CTO Offsite test
=======
[![Build Status](https://travis-ci.org/khchanel/cba-cto-offiste.svg?branch=master)](https://travis-ci.org/khchanel/cba-cto-offiste)

[Specification Here](spec.pdf)

The solution is implemented as a C# .NET core 2.1 console app

## Development setup
You will need .net core >=2.1
You may use Visual Studio or dotnet command

## Build

```
dotnet build
```

## Run
The app can be launched by issuing below command in the project root folder.  
```
dotnet run --project=CbaOffsite
```
The app will execute once and generate result, and then watch on rules.xml config file for changes.  
If it detect a change, the app will re-run with the new rules.  

Note that you will need to have Rules.xml and TestInput.txt file in current directory to work
alternatively just run in Visual Studio :)

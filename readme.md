CBA CTO Offsite test
=======


[Specification Here](spec.pdf)

The solution is implemented as a C# .NET core 2.1 console app

## Development setup
You will need .net core >=2.1


## Build

```
dotnet build
```

## Run
The app can be launched by issuing below command in the project root folder.  
```
dotnet run
```
The app will execute once and generate result, and then watch on rules.xml config file for changes.  
If it detect a change, the app will re-run with the new rules.  

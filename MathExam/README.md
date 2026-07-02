# MathExam

Минимальный учебный C# проект для МДК 02.02.

Тема: решение квадратных уравнений `ax^2 + bx + c = 0`.

## Структура

```text
MathExam/
  MathExam.sln
  MathExam.Core/
    Models/
      QuadraticEquation.cs
      EquationResult.cs
    Services/
      EquationService.cs
    FileStorage/
      CsvStorage.cs
      ExcelStorage.cs
    Diagnostics/
      DebugHelper.cs
  MathExam.App/
    Program.cs
  MathExam.Tests/
    EquationServiceTests.cs
```

## Команды создания проекта

```powershell
dotnet new sln -n MathExam -f sln
dotnet new classlib -n MathExam.Core -o MathExam.Core -f net8.0
dotnet new console -n MathExam.App -o MathExam.App -f net8.0
dotnet new mstest -n MathExam.Tests -o MathExam.Tests -f net8.0

dotnet sln .\MathExam.sln add .\MathExam.Core\MathExam.Core.csproj
dotnet sln .\MathExam.sln add .\MathExam.App\MathExam.App.csproj
dotnet sln .\MathExam.sln add .\MathExam.Tests\MathExam.Tests.csproj

dotnet add .\MathExam.App\MathExam.App.csproj reference .\MathExam.Core\MathExam.Core.csproj
dotnet add .\MathExam.Tests\MathExam.Tests.csproj reference .\MathExam.Core\MathExam.Core.csproj

dotnet add .\MathExam.Core\MathExam.Core.csproj package ClosedXML
```

## Запуск

```powershell
dotnet run --project .\MathExam.App\MathExam.App.csproj
```

После запуска создаются файлы:

- `equations.csv`
- `equations.xlsx`

## Тесты

```powershell
dotnet test .\MathExam.sln
```

## Git

```powershell
git init
git add .
git commit -m "Initial math project"

git switch -c feature/file-storage
git add .
git commit -m "Add CSV and XLSX storage"

git switch main
git merge feature/file-storage

git remote add origin https://github.com/YOUR_LOGIN/MathExam.git
git push -u origin main
```

## Текст для защиты

Я разработал консольное приложение для решения квадратных уравнений. Решение состоит из DLL-библиотеки, консольного приложения и проекта с модульными тестами. В DLL находится основная логика: модель уравнения, сервис решения, классы для работы с CSV и XLSX, а также отладочный класс DebugHelper. Консольный проект подключает DLL и использует её классы. Для проверки логики написаны модульные тесты. Для контроля версий используется Git.

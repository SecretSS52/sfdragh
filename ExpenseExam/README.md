# ExpenseExam

Минимальный учебный C# проект для подготовки к модульному экзамену по МДК 02.02.

Тема: учет расходов.

## Структура проекта

```text
ExpenseExam/
  ExpenseExam.sln
  ExpenseExam.Core/
    Models/
      Expense.cs
    Services/
      ExpenseService.cs
    FileStorage/
      CsvStorage.cs
      ExcelStorage.cs
    Diagnostics/
      DebugHelper.cs
  ExpenseExam.App/
    Program.cs
  ExpenseExam.Tests/
    ExpenseServiceTests.cs
```

## Команды создания проекта

```powershell
dotnet new sln -n ExpenseExam -f sln
dotnet new classlib -n ExpenseExam.Core -o ExpenseExam.Core -f net8.0
dotnet new console -n ExpenseExam.App -o ExpenseExam.App -f net8.0
dotnet new mstest -n ExpenseExam.Tests -o ExpenseExam.Tests -f net8.0

dotnet sln .\ExpenseExam.sln add .\ExpenseExam.Core\ExpenseExam.Core.csproj
dotnet sln .\ExpenseExam.sln add .\ExpenseExam.App\ExpenseExam.App.csproj
dotnet sln .\ExpenseExam.sln add .\ExpenseExam.Tests\ExpenseExam.Tests.csproj

dotnet add .\ExpenseExam.App\ExpenseExam.App.csproj reference .\ExpenseExam.Core\ExpenseExam.Core.csproj
dotnet add .\ExpenseExam.Tests\ExpenseExam.Tests.csproj reference .\ExpenseExam.Core\ExpenseExam.Core.csproj

dotnet add .\ExpenseExam.Core\ExpenseExam.Core.csproj package ClosedXML
```

## Запуск

```powershell
dotnet run --project .\ExpenseExam.App\ExpenseExam.App.csproj
```

После запуска создаются файлы:

- `expenses.csv`
- `expenses.xlsx`

## Тесты

```powershell
dotnet test .\ExpenseExam.sln
```

## Git

```powershell
git init
git add .
git commit -m "Initial project structure"

git switch -c feature/file-storage
git add .
git commit -m "Add CSV and XLSX file storage"

git switch main
git merge feature/file-storage

git remote add origin https://github.com/YOUR_LOGIN/ExpenseExam.git
git push -u origin main
```

## Текст для защиты

Я разработал консольное приложение для учёта расходов. Решение состоит из DLL-библиотеки, консольного приложения и проекта с модульными тестами. В DLL находится основная логика: модель расхода, сервис расчётов, классы для работы с CSV и XLSX, а также отладочный класс DebugHelper. Консольный проект подключает DLL и использует её классы. Для проверки логики написаны модульные тесты. Для контроля версий используется Git.

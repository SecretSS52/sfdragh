# Как написать такой проект самому

Ниже порядок, по которому можно повторить проект с нуля.

## 1. Создать solution

```powershell
dotnet new sln -n ExpenseExamTraining -f sln
```

## 2. Создать три проекта

```powershell
dotnet new classlib -n ExpenseTracker.Core -o src\ExpenseTracker.Core -f net8.0
dotnet new console -n ExpenseTracker.App -o src\ExpenseTracker.App -f net8.0
dotnet new mstest -n ExpenseTracker.Tests -o tests\ExpenseTracker.Tests -f net8.0
```

## 3. Добавить проекты в solution

```powershell
dotnet sln .\ExpenseExamTraining.sln add `
  .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj `
  .\src\ExpenseTracker.App\ExpenseTracker.App.csproj `
  .\tests\ExpenseTracker.Tests\ExpenseTracker.Tests.csproj
```

## 4. Подключить DLL к консоли и тестам

```powershell
dotnet add .\src\ExpenseTracker.App\ExpenseTracker.App.csproj reference .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj
dotnet add .\tests\ExpenseTracker.Tests\ExpenseTracker.Tests.csproj reference .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj
```

## 5. Добавить пакет для XLSX

```powershell
dotnet add .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj package ClosedXML
```

## 6. Написать модель

Файл:

```text
src/ExpenseTracker.Core/Models/Expense.cs
```

Модель описывает одну запись расхода: дату, категорию, сумму и описание.

## 7. Написать сервис анализа

Файл:

```text
src/ExpenseTracker.Core/Services/ExpenseAnalyzer.cs
```

Сервис считает:

- количество расходов;
- общую сумму;
- средний расход;
- количество крупных расходов;
- категорию с максимальной суммой.

## 8. Написать файловые хранилища

Файлы:

```text
IExpenseStorage.cs
CsvExpenseStorage.cs
ExcelExpenseStorage.cs
```

Интерфейс нужен, чтобы консоль могла одинаково работать с CSV и XLSX.

## 9. Добавить DebugHelper

Файл:

```text
src/ExpenseTracker.Core/Diagnostics/DebugHelper.cs
```

Он содержит два метода:

```csharp
WriteDebug(...)
WriteTrace(...)
```

И используется внутри сервисов и файловых хранилищ.

## 10. Написать консоль

Файл:

```text
src/ExpenseTracker.App/Program.cs
```

Консоль должна только вызывать готовые классы из DLL:

1. создать демо-расходы;
2. сохранить CSV;
3. загрузить CSV;
4. сохранить XLSX;
5. загрузить XLSX;
6. вывести статистику.

## 11. Написать тесты

Файлы:

```text
ExpenseAnalyzerTests.cs
FileStorageTests.cs
```

Тесты проверяют расчеты и файловый round-trip: сохранить, загрузить, сравнить.

## 12. Проверить

```powershell
dotnet test .\ExpenseExamTraining.sln
dotnet run --project .\src\ExpenseTracker.App\ExpenseTracker.App.csproj
```

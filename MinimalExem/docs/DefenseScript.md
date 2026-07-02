# Шпаргалка для защиты

Можно сказать так:

> Это учебный проект учета расходов. Он состоит из DLL, консольного приложения и проекта MSTest. Основная логика находится в `ExpenseTracker.Core`, а консоль только вызывает готовые классы из библиотеки.

Что показать:

1. `Models/Expense.cs` - модель расхода.
2. `Services/ExpenseAnalyzer.cs` - расчет статистики.
3. `FileStorage/IExpenseStorage.cs` - общий интерфейс для файлов.
4. `FileStorage/CsvExpenseStorage.cs` - CSV.
5. `FileStorage/ExcelExpenseStorage.cs` - XLSX.
6. `Diagnostics/DebugHelper.cs` - отладка через `Debug` и `Trace`.
7. `tests/ExpenseTracker.Tests` - модульные тесты MSTest.
8. `Program.cs` - консоль вызывает DLL, но не хранит бизнес-логику.

Команды:

```powershell
dotnet test .\ExpenseExamTraining.sln
dotnet run --project .\src\ExpenseTracker.App\ExpenseTracker.App.csproj
git log --oneline --decorate --graph --all
```

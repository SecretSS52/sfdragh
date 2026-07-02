# ExpenseExamTraining

Учебный C# проект для подготовки к модульному экзамену по МДК 02.02.

Тема проекта: учет расходов.

Проект специально сделан простым: модель расхода, анализ расходов, чтение и запись CSV/XLSX, консольное приложение и MSTest-тесты.

## Структура

```text
ExpenseExamTraining/
  ExpenseExamTraining.sln
  src/
    ExpenseTracker.Core/   # DLL-библиотека с основной логикой
    ExpenseTracker.App/    # консольное приложение
  tests/
    ExpenseTracker.Tests/  # MSTest-тесты
  docs/                    # краткие пояснения по проекту
```

## Что есть в DLL

Вся основная логика находится в `ExpenseTracker.Core`:

- `Models/Expense.cs` - модель расхода.
- `Services/ExpenseAnalyzer.cs` - анализ расходов.
- `Services/ExpenseSummary.cs` - результат анализа.
- `FileStorage/IExpenseStorage.cs` - общий интерфейс файлового хранилища.
- `FileStorage/CsvExpenseStorage.cs` - чтение и запись CSV.
- `FileStorage/ExcelExpenseStorage.cs` - чтение и запись XLSX.
- `Diagnostics/DebugHelper.cs` - простой отладочный класс через `Debug` и `Trace`.

## Запуск консоли

```powershell
dotnet run --project .\src\ExpenseTracker.App\ExpenseTracker.App.csproj
```

Консоль создает демо-список расходов, сохраняет его в CSV и XLSX, читает файлы обратно и выводит статистику.

## Запуск тестов

```powershell
dotnet test .\ExpenseExamTraining.sln
```

Тесты проверяют:

- общую сумму расходов;
- средний расход;
- количество расходов;
- сохранение и загрузку CSV;
- сохранение и загрузку XLSX.

## Документация

- [Карта требований](docs/RequirementsMap.md)
- [Как написать такой проект самому](docs/HowToWriteThisProject.md)
- [Git-памятка](docs/GitPractice.md)
- [Шпаргалка для защиты](docs/DefenseScript.md)

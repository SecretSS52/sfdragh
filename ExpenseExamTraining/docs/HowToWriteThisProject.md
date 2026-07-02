# Как написать такой проект самому

Это не просто описание готового проекта. Это порядок, по которому ты можешь написать похожий проект с нуля.

Примерная идея: сначала создаем структуру, потом модель, потом расчеты, потом файлы, потом консоль, потом тесты, потом Git.

## Шаг 1. Создай решение

Решение - это контейнер для нескольких проектов.

```powershell
dotnet new sln -n ExpenseExamTraining -f sln
```

## Шаг 2. Создай три проекта

DLL-библиотека:

```powershell
dotnet new classlib -n ExpenseTracker.Core -o src\ExpenseTracker.Core -f net8.0
```

Консоль:

```powershell
dotnet new console -n ExpenseTracker.App -o src\ExpenseTracker.App -f net8.0
```

Тесты MSTest:

```powershell
dotnet new mstest -n ExpenseTracker.Tests -o tests\ExpenseTracker.Tests -f net8.0
```

## Шаг 3. Добавь проекты в solution

```powershell
dotnet sln .\ExpenseExamTraining.sln add `
  .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj `
  .\src\ExpenseTracker.App\ExpenseTracker.App.csproj `
  .\tests\ExpenseTracker.Tests\ExpenseTracker.Tests.csproj
```

## Шаг 4. Подключи DLL к консоли и тестам

Консоль должна использовать классы из DLL:

```powershell
dotnet add .\src\ExpenseTracker.App\ExpenseTracker.App.csproj reference .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj
```

Тесты тоже должны проверять DLL:

```powershell
dotnet add .\tests\ExpenseTracker.Tests\ExpenseTracker.Tests.csproj reference .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj
```

Это важный экзаменационный момент: логика находится не в консоли, а в отдельной библиотеке.

## Шаг 5. Добавь пакет для Excel

Для `.xlsx` используем `ClosedXML`:

```powershell
dotnet add .\src\ExpenseTracker.Core\ExpenseTracker.Core.csproj package ClosedXML
```

## Шаг 6. Напиши модель

Модель - это класс, который описывает одну запись.

В проекте это файл:

```text
src/ExpenseTracker.Core/Models/Expense.cs
```

Смысл модели:

```csharp
public sealed record Expense(DateOnly Date, string Category, decimal Amount, string Comment);
```

В готовом проекте модель чуть подробнее: там есть русские JSON-названия и вычисляемое свойство `IsBig`.

## Шаг 7. Напиши модуль расчетов

Файл:

```text
src/ExpenseTracker.Core/Services/ExpenseAnalyzer.cs
```

Задача класса:

- посчитать количество расходов;
- посчитать общую сумму;
- посчитать средний расход;
- посчитать крупные расходы;
- найти самую дорогую категорию.

Почему это отдельный класс:

- его легко тестировать;
- он не зависит от файлов;
- он не зависит от консоли.

## Шаг 8. Напиши общий интерфейс для файлов

Файл:

```text
src/ExpenseTracker.Core/FileStorage/IExpenseStorage.cs
```

Идея:

```csharp
public interface IExpenseStorage
{
    void Save(string path, IReadOnlyCollection<Expense> expenses);
    IReadOnlyList<Expense> Load(string path);
}
```

После этого CSV, JSON и XLSX можно использовать одинаково.

## Шаг 9. Напиши CSV, JSON и XLSX модули

Файлы:

```text
CsvExpenseStorage.cs
JsonExpenseStorage.cs
ExcelExpenseStorage.cs
```

Каждый файл делает одно и то же:

1. `Save` - сохраняет список расходов.
2. `Load` - читает список расходов обратно.

Разница только в формате файла.

## Шаг 10. Добавь отладочный класс

Файлы:

```text
Debugging/IDebugLogger.cs
Debugging/ConsoleDebugLogger.cs
Debugging/ExpenseDebugger.cs
```

Зачем это нужно:

- показать отдельный отладочный модуль;
- вывести внутренние данные в Debug-сборке;
- потренироваться в замене настоящей консоли тестовым логгером.

Ключевая идея:

```csharp
#if DEBUG
    // этот код работает только в Debug-сборке
#endif
```

## Шаг 11. Напиши консольное приложение

Файл:

```text
src/ExpenseTracker.App/Program.cs
```

Консоль должна быть тонкой. Она не должна сама считать и парсить файлы.

Она только:

1. берет демо-данные;
2. вызывает DLL;
3. сохраняет файлы;
4. выводит результат.

Если вся логика оказалась в `Program.cs`, значит проект плохо разбит на модули.

## Шаг 12. Напиши тесты MSTest

Файлы:

```text
tests/ExpenseTracker.Tests/ExpenseAnalyzerTests.cs
tests/ExpenseTracker.Tests/FileStorageTests.cs
tests/ExpenseTracker.Tests/ExpenseDebuggerTests.cs
```

Как думать о тестах:

- тест расчета проверяет математику;
- тест файлов делает round-trip: сохранить -> прочитать -> сравнить;
- тест отладки проверяет, что отладчик пишет сообщения.

Запуск:

```powershell
dotnet test .\ExpenseExamTraining.sln
```

## Шаг 13. Запусти консоль

```powershell
dotnet run --project .\src\ExpenseTracker.App\ExpenseTracker.App.csproj
```

После запуска появятся файлы:

```text
expenses.csv
expenses.json
expenses.xlsx
```

## Шаг 14. Оформи Git

```powershell
git init -b main
git add .
git commit -m "Создать учебный проект учета расходов"
```

Для ветки:

```powershell
git switch -c feature/add-guide
git add .
git commit -m "Добавить учебный гайд"
git switch main
git merge feature/add-guide
```

Для GitHub:

```powershell
git remote add origin https://github.com/YOUR_LOGIN/ExpenseExamTraining.git
git push -u origin main
```

## Как переделать под другую тему

Оставь архитектуру той же, поменяй только предметную область.

Например:

- расходы -> оценки;
- `Expense` -> `Grade`;
- `ExpenseAnalyzer` -> `GradeAnalyzer`;
- CSV/JSON/XLSX-классы останутся такими же по смыслу;
- тесты останутся такими же по структуре.

Главная мысль: тема может быть любой, но схема проекта почти всегда одинаковая.

## Что говорить на защите

Короткий вариант:

> У меня есть solution из трех проектов. `ExpenseTracker.Core` - это DLL с основной логикой. Консольный проект подключает DLL и вызывает ее классы. Тестовый проект тоже подключает DLL и проверяет модули отдельно. Чтение и запись файлов вынесены в отдельные классы для CSV, JSON и XLSX. Отладочный вывод вынесен в `ExpenseDebugger` и работает в Debug-сборке. Код прокомментирован, Git-репозиторий содержит коммиты и ветки.

Это звучит просто, но закрывает почти все, что от тебя хотят.

# ExpenseExamTraining

Учебный проект для МДК 02.02: простой по смыслу, но закрывает основные требования экзамена.

Тема проекта: учет расходов.

## Что внутри

```text
ExpenseExamTraining/
  src/
    ExpenseTracker.Core/   # DLL: модели, расчеты, файлы, отладка
    ExpenseTracker.App/    # Консольное приложение, подключает DLL
  tests/
    ExpenseTracker.Tests/  # MSTest-тесты по модулям
  docs/
    HowToWriteThisProject.md
    RequirementsMap.md
    GitPractice.md
```

## Быстрый запуск

```powershell
dotnet test .\ExpenseExamTraining.sln
dotnet run --project .\src\ExpenseTracker.App\ExpenseTracker.App.csproj
```

После запуска приложение создаст:

- `expenses.csv`
- `expenses.json`
- `expenses.xlsx`

## С чего начать обучение

1. Открой [docs/RequirementsMap.md](docs/RequirementsMap.md) и посмотри, где закрыт каждый пункт экзамена.
2. Потом открой [docs/HowToWriteThisProject.md](docs/HowToWriteThisProject.md) и пройди проект по шагам.
3. Перед защитой открой [docs/DefenseScript.md](docs/DefenseScript.md) и проговори проект вслух.
4. После этого меняй тему проекта: например, вместо расходов сделай оценки, товары, задачи или заказы.

# MiniExamDemo

Очень маленький пример для МДК 02.02.

Что есть внутри:

- `src/MiniExam.Core` - DLL-библиотека.
- `src/MiniExam.App` - консольное приложение, которое подключает DLL.
- `tests/MiniExam.Tests` - MSTest-тесты.
- `GradeBook.cs` - расчеты и чтение/запись `.csv`, `.json`, `.xlsx`.
- `DebugPrinter.cs` - отладочный класс.

## Запуск

```powershell
dotnet test .\MiniExamDemo.sln
dotnet run --project .\src\MiniExam.App\MiniExam.App.csproj
```

## Git

```powershell
git status
git add .
git commit -m "Минимальный учебный проект"
git switch -c feature/test-branch
git switch master
```

Для GitHub:

```powershell
git remote add origin https://github.com/YOUR_LOGIN/MiniExamDemo.git
git push -u origin master
```

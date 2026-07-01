# ModularExamDemo

Это минимальный учебный пример для части модульного экзамена по МДК 02.02.

Проект показывает:

1. Разбиение решения на модули.
2. Чтение и запись файлов `.csv`, `.xlsx`, `.json`.
3. Базовую работу с Git: ветки, коммиты, удаленный репозиторий.
4. Наличие и использование отладочных классов.
5. Модульное тестирование через MSTest.
6. Комментирование кода.
7. DLL-проект и подключенное консольное приложение.

## Структура

```text
ModularExamDemo/
  src/
    StudentRecords.Core/        # DLL: модели, расчеты, файлы, отладка
    StudentRecords.ConsoleApp/  # Консольное приложение, которое использует DLL
  tests/
    StudentRecords.Tests/       # MSTest-тесты для DLL
```

Главная идея: бизнес-логика живет в `StudentRecords.Core`, а консольный проект только вызывает готовые классы из библиотеки.

Подробный разбор по шагам лежит здесь:

[docs/ProjectGuide.md](docs/ProjectGuide.md)

## Как запустить

```powershell
dotnet restore
dotnet build
dotnet test
dotnet run --project .\src\StudentRecords.ConsoleApp\StudentRecords.ConsoleApp.csproj
```

После запуска консольное приложение создаст файлы:

```text
students.csv
students.json
students.xlsx
```

Они появятся в папке `bin/.../data` консольного проекта.

## Какие модули смотреть

- `Models/StudentRecord.cs` - модель одной оценки студента.
- `Services/GradeAnalyzer.cs` - расчеты: средний балл, сдал/не сдал, лучший результат.
- `FileStorage/*.cs` - чтение и запись CSV, JSON, XLSX.
- `Debugging/*.cs` - отладочные классы.
- `DemoData/SampleStudents.cs` - демонстрационные данные.

## Git-шпаргалка

Создать репозиторий:

```powershell
git init
git status
git add .
git commit -m "Первый учебный вариант проекта"
```

Создать ветку для новой функции:

```powershell
git switch -c feature/add-file-storage
```

Посмотреть историю:

```powershell
git log --oneline --decorate --graph --all
```

Вернуться на главную ветку и слить изменения:

```powershell
git switch main
git merge feature/add-file-storage
```

Подключить удаленный репозиторий:

```powershell
git remote add origin https://github.com/YOUR_LOGIN/ModularExamDemo.git
git push -u origin main
```

Если ветка называется `master`, можно переименовать ее:

```powershell
git branch -M main
```

## Что можно объяснить на защите

- DLL собирается из проекта `StudentRecords.Core`.
- Консольный проект подключает DLL через `ProjectReference`.
- Тестовый проект тоже подключает DLL и проверяет ее классы.
- Для `.xlsx` используется пакет `ClosedXML`.
- `StudentRecordsDebugger` показывает пример отдельного отладочного класса.
- Комментарии `/// <summary>` объясняют назначение важных классов.

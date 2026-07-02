# Практика Git

Эта памятка нужна, чтобы ты мог показать Git на экзамене не словами, а командами.

## Базовый сценарий

Посмотреть состояние:

```powershell
git status
```

Добавить изменения:

```powershell
git add .
```

Сделать коммит:

```powershell
git commit -m "Добавить модуль работы с файлами"
```

Посмотреть историю:

```powershell
git log --oneline --decorate --graph --all
```

## Ветка

Создать ветку:

```powershell
git switch -c feature/add-tests
```

Вернуться на главную ветку:

```powershell
git switch main
```

Слить ветку:

```powershell
git merge feature/add-tests
```

## Удаленный репозиторий

Если репозиторий создан на GitHub:

```powershell
git remote add origin https://github.com/YOUR_LOGIN/ExpenseExamTraining.git
git push -u origin main
```

Проверить remote:

```powershell
git remote -v
```

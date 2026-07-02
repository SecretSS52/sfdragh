# Git-памятка

## Первый коммит

```powershell
git status
git add .
git commit -m "Обновить учебный проект под требования экзамена"
```

## Ветка

```powershell
git switch -c feature/file-storage
git status
git add .
git commit -m "Добавить файловые хранилища"
git switch main
git merge feature/file-storage
```

## Удаленный репозиторий

```powershell
git remote add origin https://github.com/YOUR_LOGIN/ExpenseExamTraining.git
git push -u origin main
git remote -v
```

# Практика Git

Этот файл специально добавлен в отдельной ветке `feature/git-practice-notes`.

Мини-сценарий для тренировки:

1. Посмотреть текущую ветку: `git branch`.
2. Посмотреть изменения: `git status`.
3. Добавить изменения: `git add .`.
4. Сделать коммит: `git commit -m "Добавить памятку по Git"`.
5. Вернуться на `main`: `git switch main`.
6. Слить ветку: `git merge feature/git-practice-notes`.

Для настоящего удаленного репозитория после создания проекта на GitHub:

```powershell
git remote add origin https://github.com/YOUR_LOGIN/ModularExamDemo.git
git push -u origin main
```

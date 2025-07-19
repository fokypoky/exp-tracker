Создание миграции. Из папки backend:
```bash
dotnet ef migrations add <название миграции> -s ExpTracker.Api -p ExpTracker.DataAccess.PostgreSQL
```

Применение миграций. Из папки backend:
```bash
dotnet ef database update -s ExpTracker.Api -p ExpTracker.DataAccess.PostgreSQL
```

# Project Management API

## Необходимые зависимости

Перед запуском проекта необходимо установить:

* .NET 10 SDK
* PostgreSQL

---

## Запуск проекта

### 1. Переход к проекту с БД

```bash
cd ./src/ProjectManagement.Infrastructure
```

### 2. Настройка базы данных

Укажите строку подключения в файле:

```text
appsettings.json
```

Пример:

```json
"ConnectionStrings": {
  "Database": "Host=localhost;Database=project_management;Username=postgres;Password=postgres;"
}
```

### 3. Применение миграций

Если не установлен `dotnet ef` выполните следующую комманду:

```bash
dotnet tool install dotnet-ef
```

Примените миграции:

```bash
dotnet ef database update -c WriteDbContext
```

### 4. Запуск приложения

Переход к проекту с API

```bash
cd ./../ProjectManagement.Api
```

```bash
dotnet run
```

После запуска API будет доступно по адресу:

```text
http://localhost:5114
```

Интерактивная документация Scalar будет доступна по адресу:

```text
http://localhost:5114/scalar
```

**Scalar требует подключение к интернету**


### 5. Начало работы

Так как система требует авторизацию для регистрации, в бд создается учетная запись администратора.

Email:

```text
admin@example.com
```

Password:

```text
admin123
```

После авторизации сервер выдаст токен, который нужно будет указать в поле `Authentication`, выбрав тип `Bearer`.

**Токен указывается строкой без кавычек и иных символов**

---

## Назначение проекта

Данный проект представляет собой backend API для системы управления проектами и задачами.

Основные цели проекта:

- демонстрация подхода Vertical Slice Architecture (VSA)
- использование CQRS
- построение чистой и масштабируемой архитектуры
- реализация аутентификации и авторизации
- управление пользователями, проектами, задачами и исполнителями

Проект создавался как учебное и демонстрационное приложение для изучения современных подходов к разработке backend-приложений на .NET.

---

## Используемые технологии

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- MediatR
- FluentValidation
- OpenAPI
- Scalar
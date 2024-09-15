# Guider PRO WEB API

- Ким Илья Григорьевич;
- Тестовое задание для С# разработчика;

Этот проект представляет собой реализацию API на платформе ASP.NET с использованием EntityFramework и Swagger для управления данными о категориях заведений, самих заведениях и тегах. Проект разработан в рамках тестового задания для кандидата на позицию стажера C# разработчика компании Guider.pro.

### Запуск проекта на локальном компьютере

Для удобного развертывания проекта в локальной среде используется Docker. В проекте присутствуют **Dockerfile** и **docker-compose.yml** для упрощения настройки и запуска всех необходимых компонентов.

1. **Убедитесь, что Docker установлен на вашей машине**.
   Если Docker еще не установлен, скачайте и установите его с [официального сайта Docker](https://www.docker.com/products/docker-desktop/).


2. **Клонируйте репозиторий с проектом и перейдите в корневую папку проекта:** 
````
git clone https://github.com/KimIlia91/guider.git
cd guider
 ````
3. **Соберите и запустите проект с помощью Docker Compose:** В корневой директории проекта выполните следующую команду в командной строке:
````
docker-compose up -d --build
````
Эта команда:

- Соберет Docker-образ приложения из Dockerfile.
- Создаст и запустит все необходимые контейнеры, такие как база данных и API-приложение.
4. **Ожидайте завершения сборки и запуска контейнеров.**
   После успешного выполнения команды приложение будет доступно по адресу:
```
http://localhost:8080
```

### Функциональные требования: 

Проект включает в себя следующие основные функции:

- Управление категориями заведений: создание, чтение, обновление и удаление (CRUD операции).
- Управление заведениями: включает связь с категориями и управление адресом, описанием и тегами.
- Управление тегами: создание, чтение, обновление и удаление с возможностью привязки к заведениям.

### Связи между сущностями:

1. Каждое заведение относится к одной категории.
2. У каждого заведения может быть несколько тегов, и каждый тег может относиться к нескольким заведениям.

### Технологии:
- **ASP.NET Core** для разработки API.
- **EntityFramework Core** для работы с базой данных.
- База данных **PostgreSQL**
- **Swagger** для документирования API.
- **Docker** для контейнеризации приложения и обеспечения простого развертывания в различных средах.

### Архитектура и применяемые паттерны:

#### ***1. Clean Architecture:***

- Проект построен на основе Clean Architecture для обеспечения разделения ответственности и улучшения тестируемости кода. Эта архитектура делит приложение на несколько слоев с чётко определёнными зависимостями, направленными внутрь:
   - **Domain слой:** Содержит бизнес-логику и сущности. Этот слой не имеет зависимостей от других частей приложения, что обеспечивает его независимость от инфраструктуры.
   - **Application слой:** Здесь реализуются бизнес-правила, сервисы и обработчики запросов. Этот слой взаимодействует с внешними источниками данных через интерфейсы.
   - **Infrastructure слой:** В этом слое сосредоточены реализации доступа к данным, взаимодействие с внешними сервисами и любые сторонние библиотеки (например, базы данных или логирование).
   - **Presentation слой (WebApi):** Взаимодействует с пользователями через API, содержит контроллеры и слои взаимодействия с клиентом.
     CQRS (Command Query Responsibility Segregation):

#### ***2. CQRS (Command Query Responsibility Segregation):***

- В проекте применён паттерн CQRS для разделения операций чтения и записи.
  - **Команды** (Commands) используются для выполнения операций изменения состояния системы (например, создание, обновление или удаление сущностей).
  - **Запросы** (Queries) используются исключительно для получения данных, что помогает четко разделить логику и улучшает производительность при масштабировании.
  - Это разделение упрощает сопровождение кода и позволяет применять разные подходы для оптимизации чтения и записи данных.

#### ***3. MediatR:***

- Для реализации CQRS использован паттерн **Mediator** с библиотекой MediatR. Этот подход позволяет:
  - Избавиться от прямой зависимости между слоями бизнес-логики и контроллерами.
  - Упрощает обработку команд и запросов через централизованного медиатора, который управляет маршрутизацией сообщений (команд и запросов) между компонентами.
  - Это также улучшает тестируемость и расширяемость приложения, так как новый функционал легко интегрируется через расширение обработчиков MediatR.

#### ***4. Dependency Injection:***

- Внедрение зависимостей (Dependency Injection) применяется для управления зависимостями между слоями и сервисами. Это улучшает модульность, делает проект более гибким и тестируемым, обеспечивая слабую связанность между компонентами.



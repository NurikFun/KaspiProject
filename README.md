# KaspiProject

1. /api/register При регистрации пользователя пароль должен включать в себя буквы заглавного, маленького, числа, другие символы (Пример : qwerty123AA!)

2. Вставка токен должна всегда передавать в загаловке слово Bearer (Пример : Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MjY0MjU2OTUsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzIxIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMjEifQ.FumWNpftGOsJH9t96Fy_k-LhWLEJV_ryTVU7z910Ktg)

3. Примеры даты from 2021/07/15 to 2021/07/16

4. При выборе архитектуры приложения руководствовался [link](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures). Хотел сделать проект привликательным с точки зрения кода, избежать сильно связанных объектов и работать на уровне абстракции, Поэтому мой выбор пал на Clean Architecture (Onion Architecture).

5. Использовал .NET Core, ибо есть удобные поддержки наподобие Swagger.

6. Основной ORM EF Core, потому что сложный и громоздких запросов не было, поэтому делал акцент в сторону функционального программирования.


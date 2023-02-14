# Решение


1) Дописал экшн UploadJson в AuctionsController. Добавил загрузку в базу. 
для добавл
2) Дописал экшн LoadData в AuctionsController для возвращения след. данных с фильтрацией по имени компании:
- Номер аукциона
- Имя компании с формой собственности
- Список лотов

   Параметр search использовать для фильтрации запроса по имени компании\


Для проверки  можно запустить приложение. Должна открыться форма, через которую можно загрузить выбранный json файл и выполнить метод LoadData (результат выполнения LoadData можно посмотреть в консоли браузера) 

## Программное обеспечение

1) Visual Studio
https://visualstudio.microsoft.com/ru/vs/

2) Node.js 14.21.2+
https://nodejs.org/en/

3) DB Browser for SQLite
https://sqlitebrowser.org/dl/

4) .net 6.0 sdk
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Установка проекта

Выполнить в папке AuctionApp
```
npm i
```

## Описание

### База данных

База с 5 таблицами Auctions.sqlite (схему можно и нужно посмотреть через DB Browser for SQLite)

Краткое описание таблиц:

1) Auctions\
Используется для хранения аукционов.

2) Companies\
Используется для хранения компаний.\
Связана с таблицей CompaniesOwnership через внешний ключ OwnershipId.

3) CompanyOwnership\
Используется для хранения форм собственности компаний.

4) Lots\
Используется для хранения информации о лотах.\
Связана с таблицей Auctions через внешний ключ AuctionId.

5) LotCompany\
Используется для связи многие ко многим между таблицами Companies и Lots.

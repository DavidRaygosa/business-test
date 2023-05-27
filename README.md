# Introduction 
Business test application. 


# Getting Started
1. Developed with Visual Studio 2022
2. Developed with Angular v15.2.8
3. Developed with Node v16.13.2
4. Developed with Bode v5.2.3
4. Developed with Fontawesome v6.2.1

# Features
.NET 6.0
Angular 15
Bootstrap 5
EF Core
Sql Server
Docker

# Make Migrations
1. Add migrations: add-migration Initial -p BusinessTest.Data -c BusinessTestDbContext -o Migrations -s BusinessTest.Data
2. Apply migrations: update-database -p BusinessTest.Data -s BusinessTest.Data

## Comments
Angular code its in "front/Angular" folder

### `production/development`
To change environment, please use 'production' or 'development' in 'appsettings.json' file

### `npm install`
Please make npm install before start the project

### `npm run prod`
To start the angular project in production mode

### `npm run dev`
To start the angular project in development mode
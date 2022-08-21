# Meetups App

## Briefing
meetups-app is a project for managing meetups

## Prerequisites
- [NodeJS v16.14.2](https://nodejs.org/download/release/v16.14.2/) 
- [.Net Core SDK v6.0.300](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- **(optional)** Some **UNIX** based OS

## SQLite Database Migrations
 - Run the following commands in order to make database migrations. Remember to first delete Migrations folder and meetups.db file
  ```
    cd ./MeetupsApp.Api
    export PATH="$PATH:$HOME/.dotnet/tools/
    dotnet ef migrations add InitialCreate
    dotnet ef database update
  ```

## How to run
- If u have some **UNIX** based OS just run `bash start_project.sh`
- Either case u have 3 options
  - Run Frontend along with the backend 
    ```
    cd ./MeetupsApp.Api
    dotnet run Program.cs
    ```
  - Run Backend Unit Tests 
    ```
    cd ./MeetupsApp.UnitTests
    dotnet test
    ```
  - Run just Frontend service 
    ```
    cd cd ./MeetupsApp.Api/ClientApp
    npm start
    ```
  - Run install dependencies for Frontend Project
    ```
    cd ./MeetupsApp.Api/ClientApp
    npm install
      ```

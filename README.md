# Meetups App

## Briefing
meetups-app is a project for managing meetups

## Prerequisites
- [NodeJS v16.14.2](https://nodejs.org/download/release/v16.14.2/) 
- [.Net Core SDK v6.0.300](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- **(optional)** Some **UNIX** based OS

## How to run
- If u have some **UNIX** based OS just run `bash start_project.sh`
- Either case u have 3 options
  - Run Frontend along with the backend 
    ```
    dotnet run Program.cs
    ```
  - Run just Frontend service 
    ```
    cd ./ClientApp
    npm start
    ```
  - Run watch test for frontend and coverage analysis
    ```
    cd ./ClientApp
    npm run test
    ```
  - Run install dependencies for Frontend Project
    ```
    cd ./ClientApp
    npm install
      ```

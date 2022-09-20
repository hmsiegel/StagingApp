# TA Aloha Staging

## Table of Contents
* [General Info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Sources](#sources)
* [Inspiration](#inspiration)

## General Info
A small application to stage TA (TravelCenters of America) Aloha hardware. Written in C# with WPF, using Caliburn Micro and Mah Apps Metro for styling. The application will determine which device it is running on and then present the user with correct UI to stage and configure that device.

## Technologies

This project is a WPF Windows application using .NET 6.0 and the Caliburn.Micro framework.

This project uses the following technologies:
* Caliburn.Micro v4.0.173
* MahApps.Metro v2.4.9
* NLog v2.7.13
* CsvHelper v27.2.1
* Dapper v2.0.123
* FluentValidation v10.3.6
* TaskScheduler v2.10.1

## Setup

This is a single file application. It is designed to run automatically on the device from the C:\AlohaStaging folder and delete itself when complete. 

## Features

### Server Staging

The application will present the user with a question of whether or not they want to sysprep the system. After sysprep runs, the server application UI will be displayed. The user will select the site number from a list (which is taken from a CSV file on the server). Once the user selects "Configure", the remainder of the process is automated.

The server will make four passes during the configuration: two while at the staging location, and two more once on site. In addition to the regular staging functions (renaming device, setting IP, setting environment variables, etc.), the application will determine based on a CSV file what Aloha features are used and will enable or disable them. It will also re-create the Aloha SQL database as this typically becomes corrupted during the sysprep process.
### Terminal Staging
 
The application will present the user with the UI upon initial startup. The user will enter in the new terminal name and IP address. After pressing the `Configure` button, the rest of the process is automated. The application will sysprep the terminal, then, after rebooting it will start configuring the terminal by setting the terminal name and IP. After that, the terminal will reboot again and start the RAL process.
### Kitchen Controller Staging

The application will present the user with the UI upon initial startup. The user will then enter the following information:
  * Controller Name
  * Controller Number
  * TERMSTR
  * Back of House Server Name
  * Controller IP Address
  * Site Key Number
  * Concept

  After entering the information and clicking `Configure` the application will make the necessary changes to the controller.

## Sources

## Inspiration

This project was initially inspired by the NCR Image Builder application. 

global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Runtime.Versioning;
global using System.Security.Principal;
global using System.Text.RegularExpressions;
global using System.Windows;
global using System.Windows.Data;
global using System.Windows.Controls;

global using Microsoft.Extensions.Configuration;

global using Autofac;
global using Autofac.Configuration;

global using AutoMapper;

global using Caliburn.Micro;
global using LogManager = Caliburn.Micro.LogManager;

global using NLog;

global using StagingApp.Application.Helpers;

global using StagingApp.Domain;
global using StagingApp.Domain.Enums;
global using StagingApp.Domain.Terminal.ValueObjects;

global using StagingApp.Presentation.Models.ConfigureModels;
global using StagingApp.Presentation.ViewModels;

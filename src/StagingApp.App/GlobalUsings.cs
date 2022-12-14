global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Runtime.Versioning;
global using System.Security.Principal;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Data;

global using Autofac;
global using Autofac.Configuration;

global using AutoMapper;
global using AutoMapper.Contrib.Autofac.DependencyInjection;

global using Caliburn.Micro;

global using MediatR;

global using Microsoft.Extensions.Configuration;

global using NLog;

global using StagingApp.Application.Shell.Queries.DetermineDevice;
global using StagingApp.Domain;
global using StagingApp.Domain.Enums;
global using StagingApp.Domain.Terminal.ValueObjects;
global using StagingApp.Presentation.Models.ConfigureModels;
global using StagingApp.Presentation.ViewModels;

global using LogManager = Caliburn.Micro.LogManager;

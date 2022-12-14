global using Autofac;

global using AutoMapper;

global using Caliburn.Micro;

global using FluentValidation;

global using MediatR;

global using Microsoft.Extensions.Configuration;

global using NLog;

global using StagingApp.Application.DeviceEnvironment.Commands.CreateRunOnceRegistryKey;
global using StagingApp.Application.DeviceEnvironment.Commands.DeleteRunRegistryKey;
global using StagingApp.Application.DeviceEnvironment.Commands.RunShutdown;
global using StagingApp.Application.DeviceEnvironment.Commands.SetAutoLogon;
global using StagingApp.Application.DeviceEnvironment.Commands.SetComputerName;
global using StagingApp.Application.DeviceEnvironment.Commands.SetIpAddress;
global using StagingApp.Application.Shell.Queries.DetermineDevice;
global using StagingApp.Application.Terminal.Commands.RunSysPrep;
global using StagingApp.Application.Terminal.Commands.SaveTerminalInfoAndSysPrep;
global using StagingApp.Application.Terminal.Commands.StartOsk;
global using StagingApp.Application.Terminal.Commands.StartRadiantAutoLoader;
global using StagingApp.Application.Terminal.Commands.StartThirdPass;
global using StagingApp.Application.Terminal.Queries.GetTerminalConfig;
global using StagingApp.Domain;
global using StagingApp.Domain.Attributes;
global using StagingApp.Domain.Common.Models;
global using StagingApp.Domain.Enums;
global using StagingApp.Domain.EventModels;
global using StagingApp.Domain.Extensions;
global using StagingApp.Domain.Services;
global using StagingApp.Domain.Terminal.ValueObjects;
global using StagingApp.Presentation.Models.ConfigureModels;
global using StagingApp.Presentation.ViewModels.Base;
global using StagingApp.Presentation.ViewModels.ConfigureViewModels;

global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Reflection;

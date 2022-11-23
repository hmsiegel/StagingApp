global using System.Runtime.Versioning;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Win32;

global using Autofac;

global using MediatR;

global using NLog;

global using StagingApp.Application.Abstractions.Messaging;
global using StagingApp.Application.Abstractions.Services;
global using StagingApp.Application.Terminal.Commands.StageTerminal;

global using StagingApp.Domain;
global using StagingApp.Domain.Errors;
global using StagingApp.Domain.Enums;
global using StagingApp.Domain.Extensions;
global using StagingApp.Domain.Network.Services;
global using StagingApp.Domain.Network.ValueObjects;
global using StagingApp.Domain.Repositories;
global using StagingApp.Domain.Server.ValueObjects;
global using StagingApp.Domain.Shared;
global using StagingApp.Domain.Terminal.ValueObjects;

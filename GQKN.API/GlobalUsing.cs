global using System;
global using System.Data.Common;
global using System.IO;
global using System.Reflection;
global using System.Collections.Generic;
global using System.Linq;
global using System.Net;
global using System.Text;
global using System.ComponentModel.DataAnnotations;
global using System.Security.Claims;
global using System.Runtime.Serialization;
global using System.Text.Json;
global using System.ComponentModel;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.OpenApi.Models;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
//global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.DependencyInjection;
//global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authorization.Infrastructure;


global using MediatR;
global using Autofac;
global using AutoMapper;
global using Autofac.Core;
global using Polly;
global using Polly.Retry;
global using Autofac.Extensions.DependencyInjection;
global using FluentValidation;

global using PVI.GQKN.BuildingBlocks.EventBus.Extensions;
global using PVI.GQKN.BuildingBlocks.IntegrationEventLogEF;
global using PVI.GQKN.BuildingBlocks.EventBus;
global using PVI.GQKN.BuildingBlocks.EventBus.Abstractions;
global using PVI.GQKN.BuildingBlocks.EventBusServiceBus;
global using PVI.GQKN.BuildingBlocks.IntegrationEventLogEF.Services;

global using PVI.GQKN.Domain.Models;
global using PVI.GQKN.Domain.Models.KBTT;
global using PVI.GQKN.Domain.Models.Identity;
global using PVI.GQKN.Domain.Seedwork;
global using PVI.GQKN.Domain.Events.KBTT;
global using PVI.GQKN.Domain.Exceptions;

global using PVI.GQKN.Infrastructure;
global using PVI.GQKN.Infrastructure.Contracts;
global using PVI.GQKN.Infrastructure.Repositories;
global using PVI.GQKN.Infrastructure.Idempotency;
global using PVI.GQKN.Infrastructure.Helpers;

global using PVI.GQKN.API.Extensions;
global using PVI.GQKN.API.Application.Exceptions;

global using PVI.GQKN.API.Infrastructure;
global using PVI.GQKN.API.Infrastructure.Filters;
global using PVI.GQKN.API.Infrastructure.ActionResults;
global using PVI.GQKN.API.Infrastructure.AutofacModules;

global using PVI.GQKN.API.Application.Queries;
global using PVI.GQKN.API.Application.Dtos;

global using PVI.GQKN.API.Application.Commands.DonViCommands;
global using PVI.GQKN.API.Application.Commands.UserCommands;
global using PVI.GQKN.API.Application.Commands.KhaiBaoTonThatCommands;
global using PVI.GQKN.API.Application.Commands.AuthCommands;
global using PVI.GQKN.API.Application.Commands.VaiTroCommands;
global using PVI.GQKN.API.Application.Commands.DanhMucCommands;

global using PVI.GQKN.API.Application.DomainEventHandlers.BCTT;
global using PVI.GQKN.API.Application.Behaviors;
global using PVI.GQKN.API.Services.ModelDTOs;
global using PVI.GQKN.API.Services;

global using PVI.GQKN.API.Services.Auth;



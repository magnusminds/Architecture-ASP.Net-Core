using Architechture.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureBuilder();

builder.Build().ConfigureWebApplication();
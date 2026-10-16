using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ToDo.Api.Data;
using ToDo.Api.Endpoints;
using ToDo.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Agrega el servicio para la conexión a la base de datos SQL server 
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapToDoItemsEndpoints();


//var todoitemsendpoints = app.mapgroup("/api/pendientes").withtags("pendientes");

//todoitemsendpoints.mapget("/",
//    async (appdbcontext dbcontext) =>
//    {
//        var todoitems = await dbcontext.todoitems.tolistasync();
//        return results.ok(todoitems);
//    });

//todoitemsendpoints.mapget("/{id:int}",
//  async (appdbcontext dbcontext, int id) =>
//    {
//        var todoitem = await dbcontext.todoitems.firstordefaultasync(tdi => tdi.id == id);
//        if (todoitem is null)
//        {
//            return results.notfound();
//        }

//        return results.ok(todoitem);
//    });

//todoitemsendpoints.mappost("api/pendientes", async (appdbcontext dbcontext, todoitem todoitem) =>
//    {
//        todoitem.iscomplete = false;
//        await dbcontext.todoitems.addasync(todoitem);
//        await dbcontext.savechangesasync();
//        return results.created($"/api/pendientes/{todoitem.id}", todoitem);
//    });

//todoitemsendpoints.mapput("/{id}",
//    async (appdbcontext dbcontext, todoitem item) =>
//    {
//        var todoitem = await dbcontext
//        .todoitems
//        .firstordefaultasync(tdi => tdi.id == item.id);
//        if (todoitem is null)
//        {
//            return results.notfound();
//        }
//        dbcontext.todoitems.attach(item);
//        dbcontext.todoitems.entry(item).state = entitystate.modified;
//        await dbcontext.savechangesasync();
//        return results.nocontent();
//    });

//todoitemsendpoints.mapdelete("/{id}",
//    async (appdbcontext dbcontext, int id) =>
//    {
//        var todoitem = await dbcontext
//        .todoitems
//        .firstordefaultasync(tdi => tdi.id == id);
//        if (todoitem is null)
//        {
//            return results.notfound();
//        }
//        dbcontext.todoitems.remove(todoitem);
//        await dbcontext.savechangesasync();
//        return results.nocontent();
//    });


app.Run();
//using Microsoft.AspNetCore.Components.Routing;
//using Microsoft.EntityFrameworkCore;
//using Scalar.AspNetCore;
//using ToDo.Api.Data;
//using ToDo.Api.Endpoints;
//using ToDo.Api.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(options =>
//options.UseSqlServer(
//    builder.Configuration
//    .GetConnectionString("DefaultConnection"))
//);

//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.MapScalarApiReference();
//}

//app.UseHttpsRedirection();
////ESTO SE AGREGO AL REALIZAR EL CAMBIO
//app.MapToDoItemsEndpoints();

//app.Run();

using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ToDo.Api.Data;
using ToDo.Api.Models;
//SE CREO LA CARPETA ENDPOINTS Y SE QUITARON DE PROGRAM 
//SE PUSIERON EN ESTA CLASE QUE TAMBIEN SE CREO
namespace ToDo.Api.Endpoints
{
    public static class ToDoItemsEndpoints
    {
        public static void MapToDoItemsEndpoints(this IEndpointRouteBuilder routes)
        {
            var toDoItemsEndpoints = routes.MapGroup("api/pendientes").WithTags("Pendientes");
            
            toDoItemsEndpoints.MapGet("",
            async (AppDbContext dbContext) =>
            {
                var toDoItem = await dbContext
                .ToDoItems.ToListAsync();
                return Results.Ok(toDoItem);
            });

            toDoItemsEndpoints.MapGet("/{id:int}",
               async (AppDbContext dbContext, int id) =>
               {
                   var toDoItem = await dbContext
           .ToDoItems
           .FirstOrDefaultAsync(tdi => tdi.Id == id);
                   if (toDoItem == null)
                   {
                       return Results.NotFound();
                   }
                   return Results.Ok(toDoItem);
               });

            toDoItemsEndpoints.MapPost("",
               async (AppDbContext dbContext, ToDoItem toDoItem) =>
               {
                   toDoItem.IsCompleted = false;
                   await dbContext.ToDoItems.AddAsync(toDoItem);
                   await dbContext.SaveChangesAsync();
                   return Results
               .Created($"/api/pendientes/{toDoItem.Id}",
               toDoItem);
               });
            //Se agrego MapPut
            toDoItemsEndpoints.MapPut("/{id}",
                async (AppDbContext dbContext, int id, ToDoItem item) =>
                {
                    var toDoItem = await dbContext
                        .ToDoItems
                        .FirstOrDefaultAsync(tdi => tdi.Id == id);
                    if (toDoItem == null)
                    {
                        return Results.NotFound();
                    }
                    //dbContext.ToDoItems.Update(item);
                    dbContext.ToDoItems.Attach(item);
                    dbContext.ToDoItems.Entry(item).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return Results.NoContent();
                });
            //------------------------------------------------------------
            //Se agrego MapDelete
            toDoItemsEndpoints.MapDelete("/{id}",
                async (AppDbContext dbContext, int id) =>
                {
                    var toDoItem = await dbContext
                        .ToDoItems
                        .FirstOrDefaultAsync(tdi => tdi.Id == id);
                    if (toDoItem == null)
                    {
                        return Results.NotFound();
                    }
                    dbContext.ToDoItems.Remove(toDoItem);
                    await dbContext.SaveChangesAsync();
                    return Results.NoContent();
                });
            //Eliminamos pendiente 3
            //--------------------------------------------------------------
        }
        
    }
}

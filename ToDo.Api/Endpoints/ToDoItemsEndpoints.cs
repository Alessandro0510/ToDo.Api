using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ToDo.Api.Data;
using ToDo.Api.DTOs;
using ToDo.Api.Models;
//SE CREO LA CARPETA ENDPOINTS Y SE QUITARON DE PROGRAM 
//SE PUSIERON EN ESTA CLASE QUE TAMBIEN SE CREO
namespace ToDo.Api.Endpoints

{
    public static class ToDoItemsEdnpoints
    {
        public static void MapToDoItemsEndpoints(this IEndpointRouteBuilder routes)
        {
            var toDoItemsEndpoints = routes.MapGroup("/api/pendientes")
                .WithTags("Pendientes");

            toDoItemsEndpoints.MapGet("/", GetAllToDoItems);
            toDoItemsEndpoints.MapGet("/{id:int}", GetToDoItemById);
            toDoItemsEndpoints.MapPost("", CreateToDoItem);
            toDoItemsEndpoints.MapPut("/{id:int}", UpdateToDoItem);
            toDoItemsEndpoints.MapDelete("/{id}", DeleteToDoItem);

        }

        private static async Task<IResult> GetAllToDoItems(
            AppDbContext dbContext)
        {
            var toDoItemsDto = await dbContext
                .toDoItems.Select( p => new ToDoItemDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsCompleted = p.IsCompleted,
                })
            .ToListAsync();
            return Results.Ok(toDoItemsDto);
        }

        private static async Task<IResult> GetToDoItemById(
            AppDbContext dbContext, int id)
        {
            var toDoitem = await dbContext.toDoItems.FirstOrDefaultAsync(tdi => tdi.Id == id);

            if (toDoitem is null)
            {
                return Results.NotFound();
            }
            var toDoItemDto = new ToDoItemDto
            {
                Id = toDoitem.Id,
                Name = toDoitem.Name,
                IsCompleted = toDoitem.IsCompleted,
            };
            return Results.Ok(toDoitem);
        }

        private static async Task<IResult>CreateToDoItem(
            AppDbContext dbContext, CreateToDoItemDto createToDoItemDto)
        {
            var toDoItem = new ToDoItem()
            {
                Name = createToDoItemDto.Name,
                IsCompleted = false
            };
            await dbContext.toDoItems.AddAsync(toDoItem);
            await dbContext.SaveChangesAsync();

            var toDoItemDto = new ToDoItemDto()
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                IsCompleted = false

            };
            return Results .Created(
               $"/api/pendientes/{toDoItem.Id}", toDoItemDto);
        }

        private static async Task<IResult> UpdateToDoItem(
            AppDbContext dbContext, int id, UpdateToDoItemDto item)
        {
            var toDoItem = await dbContext
            .toDoItems
            .FirstOrDefaultAsync(tdi => tdi.Id == id);
            if (toDoItem is null)
            {
                return Results.NotFound();
            }

            toDoItem.Name = item.Name;
            toDoItem.IsCompleted = item.IsCompleted;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        private static async Task<IResult>DeleteToDoItem(
            AppDbContext dbContext, int id) 
             {
                 var toDoItem = await dbContext
                 .toDoItems
                 .FirstOrDefaultAsync(tdi => tdi.Id == id);
                 if (toDoItem is null)
                 {
                    return Results.NotFound();
                 }
                 dbContext.toDoItems.Remove(toDoItem);
                 await dbContext.SaveChangesAsync();
                 return Results.NoContent();
                }

        }
    }


//{
//    public static class ToDoItemsEndpoints
//    {
//        public static void MapToDoItemsEndpoints( IEndpointRouteBuilder routes)
//        {
//            var toDoItemsEndpoints = routes.MapGroup("api/pendientes").WithTags("Pendientes");

//            toDoItemsEndpoints.MapGet("",
//            async (AppDbContext dbContext) =>
//            {
//                var toDoItem = await dbContext
//                .ToDoItems.ToListAsync();
//                return Results.Ok(toDoItem);
//            });

//            toDoItemsEndpoints.MapGet("/{id:int}",
//               async (AppDbContext dbContext, int id) =>
//               {
//                   var toDoItem = await dbContext
//           .ToDoItems
//           .FirstOrDefaultAsync(tdi => tdi.Id == id);
//                   if (toDoItem == null)
//                   {
//                       return Results.NotFound();
//                   }
//                   return Results.Ok(toDoItem);
//               });

//            toDoItemsEndpoints.MapPost("",
//               async (AppDbContext dbContext, ToDoItem toDoItem) =>
//               {
//                   toDoItem.IsCompleted = false;
//                   await dbContext.ToDoItems.AddAsync(toDoItem);
//                   await dbContext.SaveChangesAsync();
//                   return Results
//               .Created($"/api/pendientes/{toDoItem.Id}",
//               toDoItem);
//               });
//            //Se agrego MapPut
//            toDoItemsEndpoints.MapPut("/{id}",
//                async (AppDbContext dbContext, int id, ToDoItem item) =>
//                {
//                    var toDoItem = await dbContext
//                        .ToDoItems
//                        .FirstOrDefaultAsync(tdi => tdi.Id == id);
//                    if (toDoItem == null)
//                    {
//                        return Results.NotFound();
//                    }
//                    //dbContext.ToDoItems.Update(item);
//                    dbContext.ToDoItems.Attach(item);
//                    dbContext.ToDoItems.Entry(item).State = EntityState.Modified;
//                    await dbContext.SaveChangesAsync();
//                    return Results.NoContent();
//                });
//            //------------------------------------------------------------
//            //Se agrego MapDelete
//            toDoItemsEndpoints.MapDelete("/{id}",
//                async (AppDbContext dbContext, int id) =>
//                {
//                    var toDoItem = await dbContext
//                        .ToDoItems
//                        .FirstOrDefaultAsync(tdi => tdi.Id == id);
//                    if (toDoItem == null)
//                    {
//                        return Results.NotFound();
//                    }
//                    dbContext.ToDoItems.Remove(toDoItem);
//                    await dbContext.SaveChangesAsync();
//                    return Results.NoContent();
//                });
//            //Eliminamos pendiente 3
//            //--------------------------------------------------------------
//        }

//    }
//}

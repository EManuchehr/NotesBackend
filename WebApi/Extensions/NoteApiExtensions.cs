using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Notes.Commands.CreateNote;
using Application.Notes.Commands.DeleteNote;
using Application.Notes.Commands.UpdateNote;
using Application.Notes.Queries.GetNoteDetails;
using Application.Notes.Queries.GetNoteList;
using WebApi.Models;

namespace WebApi.Extensions;

public static class NoteApiExtensions
{
    public static WebApplication MapNotesApi(this WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");
        
        app.MapGet("/api/notes", async (IMediator mediator, ICurrentUserService currentUserService) =>
        {
            var userId = currentUserService.UserId;
            
            var query = new GetNoteListQuery()
            {
                UserId = userId
            };
            var vm = await mediator.Send(query);
        
            return Results.Ok(vm);
        }).RequireAuthorization();
        
        app.MapGet("/api/notes/{id}", async (IMediator mediator, ICurrentUserService currentUserService, Guid id) =>
        {
            var userId = currentUserService.UserId;
            
            var query = new GetNoteDetailsQuery()
            {
                UserId = userId,
                Id = id,
            };
            var vm = await mediator.Send(query);

            return Results.Ok(vm);
        }).RequireAuthorization();
        
        app.MapPost("/api/notes/", async (IMediator mediator, IMapper mapper, ICurrentUserService currentUserService, [FromForm] CreateNoteDto createNoteDto) =>
        {
            var userId = currentUserService.UserId;
            
            var command = mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = userId;
            var noteId = await mediator.Send(command);

            return Results.Ok(noteId);
        }).RequireAuthorization();
        
        app.MapPut("/api/notes/", async (IMediator mediator, IMapper mapper, ICurrentUserService currentUserService, [FromForm] UpdateNoteDto updateNoteDto) =>
        {
            var userId = currentUserService.UserId;
            
            var command = mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = userId;
            await mediator.Send(command);

            return Results.NoContent();
        }).RequireAuthorization();
        
        app.MapDelete("/api/notes/{id}", [Authorize] async (IMediator mediator, IMapper mapper, ICurrentUserService currentUserService, Guid id) =>
        {
            var userId = currentUserService.UserId;
            
            var command = new DeleteNoteCommand()
            {
                Id = id, UserId = userId,
            };
            await mediator.Send(command);

            return Results.NoContent();
        });

        return app;
    }
}
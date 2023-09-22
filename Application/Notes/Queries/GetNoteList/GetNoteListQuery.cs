using System;
using MediatR;
using Application.Notes.Queries.GetNoteDetails;

namespace Application.Notes.Queries.GetNoteList;

public class GetNoteListQuery : IRequest<NoteListVm>
{
    public Guid UserId { get; set; }
}
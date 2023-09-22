using Application.Interfaces;
using AutoMapper;
using MassTransit;
using MediatR;
using Domain.Models;
using SharedModels;

namespace Application.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly INotesDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateNoteCommandHandler(IMapper mapper, INotesDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note
        {
            UserId = request.UserId,
            Title = request.Title,
            Details = request.Details,
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            EditDate = null,
        };

        await _dbContext.Notes.AddAsync(note, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        await _publishEndpoint.Publish<INote>(note, cancellationToken);

        return note.Id;
    }
}
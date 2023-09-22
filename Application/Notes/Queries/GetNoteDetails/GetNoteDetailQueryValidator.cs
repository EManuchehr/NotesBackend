using System;
using FluentValidation;

namespace Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
    public GetNoteDetailQueryValidator()
    {
        RuleFor(command => command.Id).NotEqual(Guid.Empty);
        RuleFor(command => command.UserId).NotEqual(Guid.Empty);
    }
}
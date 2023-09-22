using FluentValidation;

namespace Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
{
    public GetNoteListQueryValidator()
    {
        RuleFor(command => command.UserId).NotEqual(Guid.Empty);
    }
}
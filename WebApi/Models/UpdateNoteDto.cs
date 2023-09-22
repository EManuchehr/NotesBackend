using System;
using AutoMapper;
using Application.Common.Mappings;
using Application.Notes.Commands.CreateNote;
using Application.Notes.Commands.UpdateNote;

namespace WebApi.Models;

public class UpdateNoteDto: IMapWith<CreateNoteCommand>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
            .ForMember(noteCommand => noteCommand.Id, opt => 
                opt.MapFrom(note => note.Id))
            .ForMember(noteCommand => noteCommand.Title, opt =>
                opt.MapFrom(note => note.Title))
            .ForMember(noteCommand => noteCommand.Details, opt =>
                opt.MapFrom(note => note.Details));
    }
}
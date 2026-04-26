using AutoMapper;
using BookLibraryApi.src.Domain.DTOs;
using BookLibraryApi.src.Domain.Models;

namespace BookLibraryApi.src.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookResponse>();
        CreateMap<CreateBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
    }
}

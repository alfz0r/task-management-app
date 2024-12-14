using AutoMapper;
using TaskManagement.Primitives;
using TaskManagement.Primitives.DTOs;

namespace TaskManagement.Core.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile() {
		CreateMap<TaskEntity, TaskEntityDto>();
		CreateMap<User, UserDto>();
	}
}

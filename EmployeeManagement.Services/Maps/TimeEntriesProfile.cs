using AutoMapper;
using Calisan_Yonetim_Core.Models;
using EmployeeManagement.Dto;

namespace EmployeeManagement.Services.Maps
{
    public class TimeEntriesProfile : Profile
    {
        public TimeEntriesProfile()
        {
            CreateMap<TimeEntries, TimeEntriesDto>()
                .ForMember(dest => dest.TimeEntriesId, opt => opt.MapFrom(src => src.TimeEntriesId))
                .ForMember(dest => dest.TimeEntriesDate, opt => opt.MapFrom(src => src.TimeEntriesDate))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectName))
                .ForMember(dest => dest.PersonnelId, opt => opt.MapFrom(src => src.PersonnelId))
                .ForMember(dest => dest.PersonnelName, opt => opt.MapFrom(src => src.Personnel.FistName + " " + src.Personnel.LastName))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<TimeEntriesDto, TimeEntries>()
                .ForMember(dest => dest.TimeEntriesId, opt => opt.MapFrom(src => src.TimeEntriesId))
                .ForMember(dest => dest.TimeEntriesDate, opt => opt.MapFrom(src => src.TimeEntriesDate))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.PersonnelId, opt => opt.MapFrom(src => src.PersonnelId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.Personnel, opt => opt.Ignore());
        }
    }
} 
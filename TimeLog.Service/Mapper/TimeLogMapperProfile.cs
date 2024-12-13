using AutoMapper;
using TimeLog.Service.Response;

namespace TimeLog.Service.Mapper
{
    public class TimeLogMapperProfile : Profile
    {
        public TimeLogMapperProfile()
        {
            CreateMap<Domain.TimeLog, TimeLogDto>();
        }
    }
}

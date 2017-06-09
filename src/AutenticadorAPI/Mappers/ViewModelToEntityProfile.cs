using AutoMapper;
using AutenticadorAPI.ViewModels;
using Profile = AutoMapper.Profile;

namespace AutenticadorAPI.Mappers
{
    public class ViewModelToEntityProfile : Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<PersonViewModel, User>()
            //    .ForMember(dest => dest.Person, opt => opt.ResolveUsing(src => new Person() { Id = src.Id, Name = src.Name }));
            //Mapper.CreateMap<PersonViewModel, Advisor>()
            //    .ForMember(dest => dest.Person, opt => opt.ResolveUsing(src => new Person() { Id = src.Id, Name = src.Name }));
            //Mapper.CreateMap<PersonViewModel, Participant>()
            //    .ForMember(dest => dest.Person, opt => opt.ResolveUsing(src => new Person() { Id = src.Id, Name = src.Name, Extra = src.Extra }));

            //Mapper.CreateMap<ProjectSaveViewModel, Project>()
            //    .ForMember(dest => dest.StartDate, opt => opt.Ignore())
            //    .ForMember(dest => dest.EndDate, opt => opt.Ignore())
            //    .ForMember(dest => dest.Advisors, opt => opt.ResolveUsing<ListAdvisorToListPersonResolver>().ConstructedBy(() => new ListAdvisorToListPersonResolver()))
            //    .ForMember(dest => dest.Participants, opt => opt.ResolveUsing<ListParticipantToListPersonResolver>().ConstructedBy(() => new ListParticipantToListPersonResolver()));
            //Mapper.CreateMap<ReportedProjectViewModel, ReportedContentProject>()
            //    .ForMember(dest => dest.Project, opt => opt.ResolveUsing(src => new Project() { Id = src.ProjectId }))
            //    .ForMember(dest => dest.Reason, opt => opt.ResolveUsing(src => new ReportedContentReason() { Id = src.ReasonId }));

            //Mapper.CreateMap<CommentViewModel, Comment>();
            //Mapper.CreateMap<ReportedCommentViewModel, ReportedContentComment>()
            //    .ForMember(dest => dest.Comment, opt => opt.ResolveUsing(src => new Comment() { Id = src.CommentId }))
            //    .ForMember(dest => dest.Reason, opt => opt.ResolveUsing(src => new ReportedContentReason() { Id = src.ReasonId }))
            //    .ForMember(dest => dest.Project, opt => opt.ResolveUsing(src => new Project() { Id = src.ProjectId }))
            //    .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

            //Mapper.CreateMap<PostViewModel, Post>();
            //Mapper.CreateMap<ReportedPostViewModel, ReportedContentPost>()
            //    .ForMember(dest => dest.Post, opt => opt.ResolveUsing(src => new Post() { Id = src.PostId }))
            //    .ForMember(dest => dest.Reason, opt => opt.ResolveUsing(src => new ReportedContentReason() { Id = src.ReasonId }))
            //    .ForMember(dest => dest.Project, opt => opt.ResolveUsing(src => new Project() { Id = src.ProjectId }))
            //    .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            //    .ForMember(dest => dest.Files, opt => opt.ResolveUsing<ListFilesPathToListFilesResolver>().ConstructedBy(() => new ListFilesPathToListFilesResolver()));

            //Mapper.CreateMap<AreaViewModel, Area>();
            //Mapper.CreateMap<TagViewModel, Tag>();
            //Mapper.CreateMap<CurriculumSubjectViewModel, CurriculumSubject>();
            //Mapper.CreateMap<ParameterViewModel, Parameter>();
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        } 
    }
}
using AutoMapper;
using Autenticador.Entities;
using AutenticadorAPI.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutenticadorAPI.Mappers
{
    //public class ListAdvisorToListPersonResolver : ValueResolver<ProjectSaveViewModel, ICollection<Advisor>>
    //{
    //    protected override ICollection<Advisor> ResolveCore(ProjectSaveViewModel source)
    //    {
    //        ICollection<Advisor> advisors = new Collection<Advisor>();

    //        if (source.Advisors != null && source.Advisors.Count > 0)
    //            foreach (var person in source.Advisors)
    //                advisors.Add(new Advisor { Person = new Person { Id = person.Id, Name = person.Name } });

    //        return advisors;
    //    }
    //}

    //public class ListParticipantToListPersonResolver : ValueResolver<ProjectSaveViewModel, ICollection<Participant>>
    //{
    //    protected override ICollection<Participant> ResolveCore(ProjectSaveViewModel source)
    //    {
    //        ICollection<Participant> participants = new Collection<Participant>();

    //        if (source.Participants != null && source.Participants.Count > 0)
    //            foreach (var person in source.Participants)
    //                participants.Add(new Participant { Person = new Person { Id = person.Id, Name = person.Name } });

    //        return participants;
    //    }
    //}

    //public class ListFilesPathToListFilesResolver : ValueResolver<ReportedPostViewModel, ICollection<ReportedContentPostFile>>
    //{
    //    protected override ICollection<ReportedContentPostFile> ResolveCore(ReportedPostViewModel source)
    //    {
    //        ICollection<ReportedContentPostFile> files = new Collection<ReportedContentPostFile>();

    //        if (source.FilesPath != null && source.FilesPath.Count > 0)
    //            foreach (var file in source.FilesPath)
    //                files.Add(new ReportedContentPostFile { File = file });

    //        return files;
    //    }
    //}
}
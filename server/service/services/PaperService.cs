using System.Collections.ObjectModel;
using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using service.interfaces;

namespace service.services;

public class PaperService(IPaperRepository paperRepository) : IPaperService
{
    public Task<SelectionWithPaginationDto<PaperDto>> GetPapers(PaperSearchDto paperSearchDto)
    {
        SelectionWithPaginationDto<Paper> papers = paperRepository.GetPaper(paperSearchDto).Result;

        SelectionWithPaginationDto<PaperDto> PaperDtos = new SelectionWithPaginationDto<PaperDto>();
        PaperDtos.Selection = ConvertPapersToPaperDtos(papers.Selection);
        PaperDtos.TotalPages = papers.TotalPages;
        
        return Task.FromResult(PaperDtos);
    }
















    private IEnumerable<PaperDto> ConvertPapersToPaperDtos(IEnumerable<Paper> papers)
    {
        IList<PaperDto> paperDtos = new List<PaperDto>();
        
        foreach (var paper in papers)
        {
            paperDtos.Add(new PaperDto(paper));
        }

        return paperDtos;
    }
}
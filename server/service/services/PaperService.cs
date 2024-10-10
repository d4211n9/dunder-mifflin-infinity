using System.Collections.ObjectModel;
using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using service.interfaces;

namespace service.services;

public class PaperService(IPaperRepository paperRepository) : IPaperService
{
    public Task<PaperDto> CreatePaper(CreatePaperDto createPaperDto)
    {
        var paperDto = new PaperDto(paperRepository.CreatePaper(createPaperDto).Result);
        return Task.FromResult(paperDto);
    }

    public Task<bool> DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto)
    {
        return paperRepository.DiscontinuePaper(discontinuePaperDto);
    }

    public Task<bool> ChangePaperStock(ChangePaperStockDto changePaperStockDto)
    {
        return paperRepository.ChangePaperStock(changePaperStockDto);
    }

    public Task<SelectionWithPaginationDto<PaperDto>> GetPapers(PaperSearchDto paperSearchDto)
    {
        SelectionWithPaginationDto<Paper> papers = paperRepository.GetPaper(paperSearchDto).Result;

        SelectionWithPaginationDto<PaperDto> PaperDtos = new SelectionWithPaginationDto<PaperDto>();
        PaperDtos.Selection = ConvertPapersToPaperDtos(papers.Selection);
        PaperDtos.TotalPages = papers.TotalPages;
        
        return Task.FromResult(PaperDtos);
    }

    public async Task<PaperDto> GetPaperById(int paperId)
    {
        Paper? paper = await paperRepository.GetPaperById(paperId);

        if (paper == null)
            throw new NullReferenceException("Could not find paper with id");

        return new PaperDto(paper);
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
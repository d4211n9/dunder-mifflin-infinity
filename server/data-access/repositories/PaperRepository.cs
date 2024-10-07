using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace data_access.repositories;

public class PaperRepository(MyDbContext myDbContext) : IPaperRepository
{
    public Task<Paper> CreatePaper(CreatePaperDto createPaperDto)
    {
        throw new NotImplementedException();
    }

    public Task DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto)
    {
        throw new NotImplementedException();
    }

    public Task ChangePaperStock(ChangePaperStockDto changePaperStockDto)
    {
        throw new NotImplementedException();
    }

    public Task<SelectionWithPaginationDto<Paper>> GetPaper(PaperSearchDto paperSearchDto)
    {
        IEnumerable<Paper> filteredPapers = myDbContext.Papers
            .Include(paper => paper.Properties)
            .Where(paper => paper.Price <= paperSearchDto.MaxPrice &&   
                            paper.Price >= paperSearchDto.MinPrice &&
                            paper.Stock >= paperSearchDto.MinStock &&
                            (paperSearchDto.ShowDiscontinued || paperSearchDto.ShowDiscontinued == false && paper.Discontinued == false)&&
                            paper.Name.ToLower().Contains(paperSearchDto.NameSearchQuery.ToLower()));

        return Task.FromResult(new SelectionWithPaginationDto<Paper>
        {
            Selection = filteredPapers
                .Skip((paperSearchDto.PaginationDto.PageNumber - 1) * paperSearchDto.PaginationDto.PageSize)
                .Take(paperSearchDto.PaginationDto.PageSize),
            TotalPages = (int) Math.Ceiling((double) filteredPapers.Count() / paperSearchDto.PaginationDto.PageSize)
        });
    }

    public async Task<double> GetPriceOfPaperFromPaperId(int paperId)
    {
        Paper? paper = await myDbContext.Papers.FirstOrDefaultAsync(paper => paper.Id == paperId);
        
        return paper.Price;
    }
















    private bool ShowDiscontinued(bool showDiscontinued, bool isDiscontinued)
    {
        if (showDiscontinued == false && isDiscontinued == false) return true;

        if (showDiscontinued) return true;

        return false;
    }
}
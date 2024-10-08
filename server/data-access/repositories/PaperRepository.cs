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
        EntityEntry<Paper> createdPaper = myDbContext.Papers.Add(new Paper
        {
            Name = createPaperDto.Name,
            Discontinued = false,
            Stock = createPaperDto.Stock,
            Price = createPaperDto.Price,
        });

        myDbContext.SaveChanges();

        return Task.FromResult(createdPaper.Entity);
    }

    public async Task<bool> DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto)
    {
        Paper paperToChangeDicontinuedOf = 
            myDbContext.Papers.FirstOrDefault(paper => paper.Id == discontinuePaperDto.PaperId);

        Paper updatedPaper = new Paper
        {
            Id = paperToChangeDicontinuedOf.Id,
            Name = paperToChangeDicontinuedOf.Name,
            Discontinued = discontinuePaperDto.Discontinue,
            Stock = paperToChangeDicontinuedOf.Stock,
            Price = paperToChangeDicontinuedOf.Price
        };

        myDbContext.Entry(paperToChangeDicontinuedOf).CurrentValues.SetValues(updatedPaper);

        return await myDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> ChangePaperStock(ChangePaperStockDto changePaperStockDto)
    {
        Paper paperToChangeStockOf = 
            myDbContext.Papers.FirstOrDefault(paper => paper.Id == changePaperStockDto.PaperId);

        Paper updatedPaper = new Paper
        {
            Id = paperToChangeStockOf.Id,
            Name = paperToChangeStockOf.Name,
            Discontinued = paperToChangeStockOf.Discontinued,
            Stock = changePaperStockDto.ChangedStock,
            Price = paperToChangeStockOf.Price
        };

        myDbContext.Entry(paperToChangeStockOf).CurrentValues.SetValues(updatedPaper);

        return await myDbContext.SaveChangesAsync() > 0;
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
}
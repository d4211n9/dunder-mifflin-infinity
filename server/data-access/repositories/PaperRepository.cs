using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace data_access.repositories;

public class PaperRepository(MyDbContext myDbContext) : IPaperRepository
{
    public Paper CreatePaper(CreatePaperDto createPaperDto)
    {
        throw new NotImplementedException();
    }

    public void DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto)
    {
        throw new NotImplementedException();
    }

    public void ChangePaperStock(ChangePaperStockDto changePaperStockDto)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Paper> GetPaper(PaperSearchDto paperSearchDto)
    {
        throw new NotImplementedException();
    }

    public async Task<double> GetPriceOfPaperFromPaperId(int paperId)
    {
        Paper? paper = await myDbContext.Papers.FirstOrDefaultAsync(paper => paper.Id == paperId);
        
        return paper.Price;
    }
}
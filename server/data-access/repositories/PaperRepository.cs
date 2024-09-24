using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;

namespace data_access.repositories;

public class PaperRepository : IPaperRepository
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
}
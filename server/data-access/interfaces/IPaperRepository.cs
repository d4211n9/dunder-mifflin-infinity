using data_access.data_transfer_objects;
using data_access.models;

namespace data_access.interfaces;

public interface IPaperRepository
{
    Task<Paper> CreatePaper(CreatePaperDto createPaperDto);
    Task DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto);
    Task ChangePaperStock(ChangePaperStockDto changePaperStockDto);
    Task<SelectionWithPaginationDto<Paper>> GetPaper(PaperSearchDto paperSearchDto);

    Task<double> GetPriceOfPaperFromPaperId(int paperId);
}
using data_access.data_transfer_objects;
using data_access.models;

namespace data_access.interfaces;

public interface IPaperRepository
{
    Task<Paper> CreatePaper(CreatePaperDto createPaperDto);
    Task<bool> DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto);
    Task<bool> ChangePaperStock(ChangePaperStockDto changePaperStockDto);
    Task<SelectionWithPaginationDto<Paper>> GetPaper(PaperSearchDto paperSearchDto);
    Task<double> GetPriceOfPaperFromPaperId(int paperId);
    Task<Paper?> GetPaperById(int paperId);
}
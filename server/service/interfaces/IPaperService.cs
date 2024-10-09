using data_access.data_transfer_objects;

namespace service.interfaces;

public interface IPaperService
{
    Task<PaperDto> CreatePaper(CreatePaperDto createPaperDto);
    Task<bool> DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto);
    Task<bool> ChangePaperStock(ChangePaperStockDto changePaperStockDto);
    Task<SelectionWithPaginationDto<PaperDto>> GetPapers(PaperSearchDto paperSearchDto);
    Task<PaperDto> GetPaperById(int paperId);
}
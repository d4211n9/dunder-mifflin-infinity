using data_access.data_transfer_objects;
using data_access.models;

namespace data_access.interfaces;

public interface IPaperRepository
{
    Paper CreatePaper(CreatePaperDto createPaperDto);
    void DiscontinuePaper(DiscontinuePaperDto discontinuePaperDto);
    void ChangePaperStock(ChangePaperStockDto changePaperStockDto);
    IEnumerable<Paper> GetPaper(PaperSearchDto paperSearchDto);
}
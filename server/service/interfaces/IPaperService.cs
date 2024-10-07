using data_access.data_transfer_objects;

namespace service.interfaces;

public interface IPaperService
{
    Task<SelectionWithPaginationDto<PaperDto>> GetPapers(PaperSearchDto paperSearchDto);
}
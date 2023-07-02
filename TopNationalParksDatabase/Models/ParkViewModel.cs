using X.PagedList.Mvc.Core;
using X.PagedList.Mvc;
using X.PagedList;
namespace TopNationalParksDatabase.Models

{
    public class ParkViewModel
    {
        public Park Park { get; set; }
        public IPagedList<Park>IPagedList { get; set; }
        public bool DisplayLinkToFirstPage { get; set; }
        public bool DisplayLinkToLastPage { get; set; }
        public bool DisplayLinkToPreviousPage { get; set; }
        public bool DisplayLinkToNextPage { get; set; }
    }
}

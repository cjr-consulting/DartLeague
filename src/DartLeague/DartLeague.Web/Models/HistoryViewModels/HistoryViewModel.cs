using System.Collections.Generic;

namespace DartLeague.Web.Models.HistoryViewModels
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
    public class HistoryViewModel
    {
        public HistorySeasonViewModel PreviousSeason { get; set; }
        public HistorySeasonViewModel NextSeason { get; set; }

        public string Title { get; set; }
        public List<HistoryDocumentViewModel> Documents { get; set; } = new List<HistoryDocumentViewModel>();
        public List<HistoryBoardMemberViewModel> BoardMembers { get; set; } = new List<HistoryBoardMemberViewModel>();
    }
}
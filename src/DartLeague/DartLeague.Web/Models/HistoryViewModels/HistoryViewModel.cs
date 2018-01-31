using System.Collections.Generic;

namespace DartLeague.Web.Models.HistoryViewModels
{
    public class HistoryViewModel
    {
        public HistorySeasonViewModel PreviousSeason { get; set; }
        public HistorySeasonViewModel NextSeason { get; set; }

        public string Title { get; set; }
        public List<HistoryDocumentViewModel> Documents { get; set; } = new List<HistoryDocumentViewModel>();
        public List<HistoryBoardMemberViewModel> BoardMembers { get; set; } = new List<HistoryBoardMemberViewModel>();
    }
}
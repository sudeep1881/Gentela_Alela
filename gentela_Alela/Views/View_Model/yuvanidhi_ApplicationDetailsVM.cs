using gentela_Alela.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gentela_Alela.Views.View_Model
{
    public class yuvanidhi_ApplicationDetailsVM
    {
        public YuvanidhiApplicantDetail applicantReg { get; set; } = new();

        public EducationDetail educationReg { get; set; } = new();

        public DomicileVerification domicileReg { get; set; } = new();

        public CommunicationDetail communicateReg { get; set; } = new();

        public AddtionalDetail additionalReg { get; set; } = new();
        public IEnumerable<SelectListItem> KADistictReg { get; set; } = Enumerable.Empty<SelectListItem>(); 
        public IEnumerable<SelectListItem> KATaluk { get; set; } = Enumerable.Empty<SelectListItem>(); 

    }
}

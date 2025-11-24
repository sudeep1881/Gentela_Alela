using gentela_Alela.Models;

namespace gentela_Alela.DTOs
{
    public class YuvanidhiApplicationListDTOs
    {
        public int IdDTOs { get; set; }

        public string? AdharNameDTOs { get; set; }

        public DateOnly? DobDTOs { get; set; }

        public string? GenderDTOs { get; set; }

        public string? PhotoDTOs { get; set; }

        public string? PernmentAdressDTOs { get; set; }


        public string? DistinctDTOs { get; set; }

        public string? TalukDTOs { get; set; }

        public string? PincodeDTOs { get; set; }


        public string? CourseDTOs { get; set; }
        public string? LevelDTOs { get; set; }
        public int? PassoutDTOs { get; set; }
        public string? UniversityDTOs { get; set; }
        public string? RegisterNODTOs { get; set; }

        public string? DomicineName { get; set; }
        public string? DomicineCardNo { get; set; }

        public string? MobileNoDTOs { get; set; }
        public string? EmailDTOs { get; set; }

        public string? CategoryDTOs { get; set; }
        public string? DisabilityDTOs { get; set; }
        public string? UDIDnbrDTOs { get; set; }

        public virtual AddtionalDetail? Additional { get; set; }

        public virtual CommunicationDetail? Communication { get; set; }

        public virtual District? Distinct { get; set; }

        public virtual DomicileVerification? Domicine { get; set; }

        public virtual EducationDetail? Education { get; set; }

        public virtual Taluk? Taluk { get; set; }
    }
}

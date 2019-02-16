namespace HMS.Common.Filters
{
    public class MedicalRecordFilter : Filter
    {
        public string Code { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string IdentifyCardNo { get; set; }
    }
}

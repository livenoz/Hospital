using System;

namespace HMS.Common.Dtos.Patient
{
    public class DiseaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
    }
}

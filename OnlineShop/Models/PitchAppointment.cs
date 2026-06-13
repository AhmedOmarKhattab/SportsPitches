namespace FiveStadium.Models
{
    public class PitchAppointment
    {
        public int Id { get; set; }
        public DateOnly Date {  get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public bool IsAvailable { get; set; }
        public int PitchId { get; set; }
        public Pitch Pitch { get; set; }
    }
}

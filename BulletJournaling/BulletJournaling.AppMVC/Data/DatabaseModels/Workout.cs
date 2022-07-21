namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class Workout
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool didWorkout { get; set; }
        public string Type { get; set; }
        public DateOnly Date { get; set; }
        public int DurationMintues { get; set; }
    }
}

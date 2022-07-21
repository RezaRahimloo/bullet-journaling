namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class Smoking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool DidSmoke { get; set; }
        public int Number { get; set; } = 0;
        public DateOnly Date { get; set; }
    }
}

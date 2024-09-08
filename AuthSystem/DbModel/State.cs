namespace AuthSystem.DbModel
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}

namespace ProjektAPI1.Enitites
{
    public class Actor
    {
        public int Id { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set;} = string.Empty;    }
}

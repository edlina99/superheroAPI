namespace SuperHeroAPI.Data
{
    public class Log
    {
        public int Id { get; set; }
        public string Action_Type { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; }
        public string Created_By { get; set; }
        public DateTime Updated_At { get; set; }
        public string Updated_By { get; set; }

    }
}

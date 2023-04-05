namespace SuperHeroAPI.ViewModels
{
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string? ErrorDescription { get; set; }
        public Object? Result { get; set; }
    }
}

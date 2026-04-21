namespace gentela_Alela.Models
{
    public class RouteLink
    {
        public int Id { get; set; }

        public string Key { get; set; } = Guid.NewGuid().ToString();

        public string Controller { get; set; }

        public string Action { get; set; }

        public string? ParamId { get; set; }
    }
}

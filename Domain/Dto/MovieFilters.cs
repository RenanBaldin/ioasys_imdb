namespace Domain.Dto
{
    public class MovieFilters : FiltersBase
    {
        public string Diretor { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        public string Actor { get; set; }
    }
}
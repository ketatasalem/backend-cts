namespace Labo_Cts_backend.Shared.Models
{
    public class FilterQuery
    {
        public int PageNumber { get; set; } = 1; // Page actuelle
        public int PageSize { get; set; } = 10; // Nombre d'éléments par page
        // Filtres spécifiques aux colonnes (clé : nom de la colonne, valeur : texte du filtre)
        public Dictionary<string, string>? Filters { get; set; } = new Dictionary<string, string>();

        public FilterQuery()
        {
        }

        public FilterQuery(int pageNumber, int pageSize, Dictionary<string, string> filters)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 100 ? 100 : pageSize;
            Filters = filters ?? new Dictionary<string, string>();
        }
    }
}

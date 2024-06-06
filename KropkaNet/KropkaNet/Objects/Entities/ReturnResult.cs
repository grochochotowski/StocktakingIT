namespace KropkaNet.Objects.Entities
{
    public class ReturnResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public ReturnResult(List<T> list, int total)
        {
            Items = list;
            TotalItems = total;
            TotalPages = total/10 + 1;
        }
    }
}

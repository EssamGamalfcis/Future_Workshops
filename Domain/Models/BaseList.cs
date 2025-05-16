public record BaseList
{
    public BaseList() { }
    public BaseList(int limit, int page, string? orderBy, string? sort)
    {
        Limit = limit;
        Page = page;
        OrderBy = orderBy;
        Sort = sort;
    }
    public int Limit { get; set; } = 20;
    public int Page { get; set; } = 1;
    public string? OrderBy { get; set; } = "id";
    public string? Sort { get; set; } = "asc";

}

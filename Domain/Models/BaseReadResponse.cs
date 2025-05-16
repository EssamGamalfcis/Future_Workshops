namespace TaskManagement.BaseResponse
{
    public class Meta
    {
        public int TotalItems { get; set; }
        public int ItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class BaseReadResponse
    {
        public object Items { get; set; }
        public Meta Meta { get; set; }
    }

    public class BaseReadResponseWithExtraData : BaseReadResponse
    {
        public object ExtraData { get; set; }
    }

    public class BaseWriteResponse
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public dynamic Data { get; set; }

    }
}

namespace ASPNETCOREWebAPI.Models
{
    public class MyDbContext
    {
        public static Task<Book?> GetByIdAsync(long id)
        {
            var result = GetById(id);
            return Task.FromResult(result);
        }
        public static Book? GetById(long id)
        {
            switch (id)
            {
                case 0:
                    return new Book(0, "零基础学习1");
                case 1:
                    return new Book(1, "书记213阿松大");
                case 2:
                    return new Book(2, "按时打算看");
                default:
                    return null;
            }
        }
    }
}
